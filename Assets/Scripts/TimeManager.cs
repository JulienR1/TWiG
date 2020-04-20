using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour, IManager
{
    public static event Action OnTick;
    public static event Action OnNextSeason;
    public static event Action OnNextDay;

    public enum Season { SPRING, SUMMER, AUTUMN, WINTER };
    private Season currentSeason = Season.SPRING;

    [SerializeField] private float secondsPerGameTick = 2;
    [SerializeField] private float ticksPerDay = 180;
    [SerializeField] private int daysInSeason = 10;

    private static bool timeIsEnabled;
    private static bool TimeIsEnabled { get => timeIsEnabled; }

    private int tick = 0;
    private int currentDay = 0;
    private float tickToDayStart = 0;
    private float timeToNextTick = 0;

    public void Initialize()
    {
        this.tick = 0;
        this.currentSeason = Season.SPRING;
        this.currentDay = 1;
        this.tickToDayStart = ticksPerDay;
        this.timeToNextTick = secondsPerGameTick;
        timeIsEnabled = true;        
    }

    private void Update()
    {
        if (!TimeIsEnabled)
            return;

        if (timeToNextTick <= 0)
            Tick();

        timeToNextTick -= Time.deltaTime;
    }

    private void Tick()
    {
        tick++;
        timeToNextTick = secondsPerGameTick;

        if (tick >= tickToDayStart)
        {
            NextDay();
            if ((currentDay + daysInSeason / 2) % daysInSeason == 0)
            {
                NextSeason();
            }
        }

        OnTick?.Invoke();
    }

    private void Pause() => timeIsEnabled = false;
    private void Resume() => timeIsEnabled = true;
    private void TogglePause() => timeIsEnabled = !timeIsEnabled;

    private void NextDay()
    {
        currentDay++;
        tickToDayStart = tick + ticksPerDay;
        OnNextDay?.Invoke();
    }

    private void NextSeason()
    {
        currentSeason = (Season)(((int)currentSeason + 1) % Enum.GetValues(typeof(Season)).Length);
        OnNextSeason?.Invoke();
    }
    
    public float LigthTime()
    {
        float yearProgress = currentDay / (float)daysInSeason;
        return ticksPerDay / 4f * (2 + Mathf.Sin(Mathf.PI * yearProgress / 2f));
    }

    public float RevolutionTime()
    {
        return ticksPerDay;
    }

    public float RevolutionPercent()
    {
        return 1 - (tickToDayStart - tick - 1 + timeToNextTick / secondsPerGameTick) / ticksPerDay;
    }

    public float SeasonPercent()
    {
        int seasonCount = Enum.GetValues(typeof(Season)).Length;
        return ((currentDay - 1) % (daysInSeason * seasonCount) + RevolutionPercent()) / (float)(daysInSeason * seasonCount);
    }

    public Season GetSeason()
    {
        return currentSeason;
    }
}
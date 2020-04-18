using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour, IManager
{
    public static event Action OnNextSeason;
    public static event Action OnNextDay;

    public enum Season { SPRING, SUMMER, AUTUMN, WINTER };
    private Season currenSeason = Season.SPRING;

    [SerializeField] private float timePerDay = 180;
    [SerializeField] private int daysInSeason = 10;

    private static bool timeIsEnabled;
    private static bool TimeIsEnabled { get => timeIsEnabled; }

    private int currentDay = 0;
    private float timeToNextDayStart = 0;

    public void Initialize()
    {
        this.currenSeason = Season.SPRING;
        this.currentDay = 1;
        this.timeToNextDayStart = timePerDay;
        timeIsEnabled = true;        
    }

    private void Update()
    {
        if (!TimeIsEnabled)
            return;

        if (timeToNextDayStart <= 0)
        {
            NextDay();
            if (currentDay % daysInSeason == 0)
            {
                NextSeason();
            }
        }
        timeToNextDayStart -= Time.deltaTime;
    }

    private void Pause() => timeIsEnabled = false;
    private void Resume() => timeIsEnabled = true;
    private void TogglePause() => timeIsEnabled = !timeIsEnabled;

    private void NextDay()
    {
        currentDay++;
        timeToNextDayStart = timePerDay;
        OnNextDay?.Invoke();
    }

    private void NextSeason()
    {
        currenSeason = (Season)(((int)currenSeason + 1) % Enum.GetValues(typeof(Season)).Length);
        OnNextSeason?.Invoke();
    }
    
    private float LigthTime()
    {
        float yearPercent = (currentDay / daysInSeason + (int)currenSeason);
        return timePerDay / 3f * (1 + 0.5f * Mathf.Sin(0.5f * (float)Math.PI * yearPercent) + 1);
    }

    private float DarkTime()
    {
        return timePerDay - LigthTime();
    }
}
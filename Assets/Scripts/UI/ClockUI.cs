using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    public RectTransform seasonClock;
    public RectTransform timeClock;
    public Image dayOverlay;

    private void Start()
    {
        TimeManager.OnNextDay += OnNewDay;
        OnNewDay();
    }

    private void Update()
    {

    }

    private void OnNewDay()
    {
        SetLightRatio();
    }

    private void OnNewSeason()
    {

    }

    private void SetLightRatio()
    {
        print(Game.instance.timeManager.LigthTime());
        dayOverlay.fillAmount = Game.instance.timeManager.LigthTime() / Game.instance.timeManager.TotalTime();
    }
}

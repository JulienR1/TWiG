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
        UpdateClockRotation();
    }

    private void UpdateClockRotation()
    {
        timeClock.rotation = Quaternion.Euler(Vector3.forward * -360 * Game.instance.timeManager.RevolutionPercent());
        seasonClock.rotation = Quaternion.Euler(Vector3.forward * 360 * Game.instance.timeManager.SeasonPercent());
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
        dayOverlay.fillAmount = Game.instance.timeManager.LigthTime() / Game.instance.timeManager.RevolutionTime();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] private Transform downArrow = null;
    [SerializeField] private Transform upArrow = null;
    [SerializeField] private Transform check = null;

    public void UpdateUI(float distanceToAverage)
    {
        upArrow.gameObject.SetActive(false);
        downArrow.gameObject.SetActive(false);
        check.gameObject.SetActive(false);

        if (distanceToAverage >= 1)
        {
            upArrow.gameObject.SetActive(true);
        }
        else if (distanceToAverage <= -1)
        {
            downArrow.gameObject.SetActive(true);
        }
        else
        {
            check.gameObject.SetActive(true);
        }
    }
}
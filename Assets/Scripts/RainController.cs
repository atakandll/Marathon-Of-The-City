using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.RainMaker;
using UnityEngine;
using Random = UnityEngine.Random;

public class RainController : MonoBehaviour
{
    private RainScript2D raincontroller => GetComponent<RainScript2D>();
    [Range(0.0f, 1.0f)]
    [SerializeField] private float intensity;
    [SerializeField] private float targetInstensity;
    [SerializeField] private float changeRate = .05f;
    [SerializeField] private float minValue = .2f;
    [SerializeField] private float maxValue = .49f;
    [SerializeField] private float changeToRain = 40;
    [SerializeField] private float rainCheckCoolDown;
    private float rainCheckTimer;

    private bool canChangeIntensity;

    private void Update()
    {
        rainCheckTimer -= Time.deltaTime;
        raincontroller.RainIntensity = intensity;
        if (Input.GetKeyDown(KeyCode.R))
        {
            canChangeIntensity = true;
        }

        CheckForRain();

        if (canChangeIntensity)
        {
            ChangeIntensity();
        }


    }
    private void CheckForRain()
    {
        if (rainCheckTimer < 0)
        {
            rainCheckTimer = rainCheckCoolDown;
            canChangeIntensity = true;

            if (Random.Range(0, 100) < changeToRain)
            {
                targetInstensity = Random.Range(minValue, maxValue);

            }
            else
            {
                targetInstensity = 0;
            }


        }
    }

    private void ChangeIntensity()
    {
        if (intensity < targetInstensity)
        {
            intensity += changeRate * Time.deltaTime;

            if (intensity >= targetInstensity)
            {
                intensity = targetInstensity;
                canChangeIntensity = false;
            }
        }
        if (intensity > targetInstensity)
        {
            intensity -= changeRate * Time.deltaTime;

            if (intensity <= targetInstensity)
            {
                intensity = targetInstensity;
                canChangeIntensity = false;
            }
        }
    }
}

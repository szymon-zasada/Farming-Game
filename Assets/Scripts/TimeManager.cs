using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Light _sun;
    [SerializeField] private float _secondsInFullDay = 120f;
    [Range(0, 1)] [SerializeField] private float _currentTimeOfDay = 0f;
    [SerializeField] private float _minIntensity = 0f;
    [SerializeField] private float _maxIntensity = 1f;
    [SerializeField] private Color _fullIntensityColor;
    [SerializeField] private Color _sunsetColor;
    [SerializeField] private float _sunriseTime = 0.25f;
    [SerializeField] private float _sunriseDuration = 0.2f;
    [SerializeField] private float _sunsetTime = 0.75f;
    [SerializeField] private float _sunsetDuration = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        _sun = GameObject.Find("Sun").GetComponent<Light>();
        ItemList.LoadItems();
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSun();
        _currentTimeOfDay += (Time.deltaTime / _secondsInFullDay);
        if (_currentTimeOfDay >= 1)
        {
            _currentTimeOfDay = 0;
        }
    }

    public static void SetTime(float time)
    {
        var timeManager = FindObjectOfType<TimeManager>();
        timeManager._currentTimeOfDay = time;
    }

    void UpdateSun()
    {
        _sun.transform.localRotation = Quaternion.Euler((_currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        
        //night
        if (_currentTimeOfDay <= _sunriseTime || _currentTimeOfDay >= _sunsetTime + _sunsetDuration)
        {
            intensityMultiplier = 0;
     
        }

        //sunrise
        else if (_currentTimeOfDay <= _sunriseTime + _sunriseDuration)
        {
            intensityMultiplier = Mathf.Clamp((_currentTimeOfDay - _sunriseTime) * (1 / _sunriseDuration), _minIntensity, _maxIntensity);
            
        }
        else if (_currentTimeOfDay >= _sunsetTime)
        {
            intensityMultiplier = Mathf.Clamp(1 - ((_currentTimeOfDay - _sunsetTime) * (1 / _sunsetDuration)), _minIntensity, _maxIntensity);
            //1 - (0.75 - 0.75) * (1 / 0.2) = 1 - 0 * 5 = 1
            //1 - (0.76 - 0.75) * (1 / 0.2) = 1 - 0.01 * 5 = 0.95
           // intensityMultiplier = 1 - ((_currentTimeOfDay - _sunsetTime) * (1 / _sunsetDuration));
        }



        _sun.intensity = intensityMultiplier;
        _sun.color = Color.Lerp(_sunsetColor, _fullIntensityColor, intensityMultiplier);
    }
}

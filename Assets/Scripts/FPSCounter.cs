using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;

    private float fpsCounter = 0;
    private float currentFpsTime = 0;
    private float fpsShowPeriod = 1;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }


    private void Update()
    {
        currentFpsTime = currentFpsTime + Time.deltaTime;
        fpsCounter = fpsCounter + 1;
        if (currentFpsTime > fpsShowPeriod)
        {
            fpsText.text = "FPS: " + fpsCounter.ToString();
            currentFpsTime = 0;
            fpsCounter = 0;
        }
    }







}

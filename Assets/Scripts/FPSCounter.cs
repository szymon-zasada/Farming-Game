using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;
    private float fpsSum;
    private int fpsCount;

    private async void Start()
    {
        while (true)
        {
            fpsSum += 1.0f / Time.deltaTime;
            fpsCount++;
            await Task.Delay(100);
        }
    }

    private void Update()
    {
        fpsText.text = "FPS: " + (fpsSum / fpsCount).ToString("0.0");
    }




}

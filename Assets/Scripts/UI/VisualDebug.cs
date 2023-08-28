using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualDebug : MonoBehaviour
{
    [SerializeField] private TMP_Text _debugText;


    void Start()
    {
   
    }

    public static void Log(string message)
    {
        var visualDebug = FindObjectOfType<VisualDebug>();
        visualDebug._debugText.text += message;
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] public RectTransform Background;
    [SerializeField] public RectTransform Container;


    public virtual void Start()
    {
        Background = transform.Find("Background").GetComponent<RectTransform>();
        Container = transform.Find("Container").GetComponent<RectTransform>();

       
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public ChestPanel _chestPanel { get; private set;}
    //[SerializeField] private GameObject _inventoryPanel;
    
    private void Start()
    {
        _chestPanel = GetComponentInChildren<ChestPanel>();
       // _chestPanel.gameObject.SetActive(false);
        //_inventoryPanel.SetActive(false);
    }

    



}

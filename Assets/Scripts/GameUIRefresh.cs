using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIRefresh : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private InventoryPanel _inventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        _canvas.SetActive(false);

        _canvas.SetActive(true);

        _inventoryPanel.Refresh();

    }

    // Update is called once per frame
    void Update()
    {
        _canvas.SetActive(true);
    }
}

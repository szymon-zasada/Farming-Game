using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }


    [SerializeField] public Inventory PlayerInventory { get; private set; } = new Inventory();


    [SerializeField] private InventoryPanel _inventoryPanel;


    void Start()
    {
        _inventoryPanel = FindObjectOfType<InventoryPanel>();


        AddItemToPlayerInventory(
            new Hoe()
        );

        AddItemToPlayerInventory(
            new Hoe()
        );

        AddItemToPlayerInventory(new Seeds());

    }



    void AddItemToPlayerInventory(Item item)
    {
        PlayerInventory.AddItem(item);
        item.OnItemDestroyed += () => _inventoryPanel.Refresh();
        _inventoryPanel.Refresh();
    }


    void AddItemsToPlayerInventory(List<Item> items)
    {
        foreach (var item in items)
        {
            PlayerInventory.AddItem(item);
            item.OnItemDestroyed += () => _inventoryPanel.Refresh();
        }
        _inventoryPanel.Refresh();
    }

}

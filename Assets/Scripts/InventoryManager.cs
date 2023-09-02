using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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

        InventoryPanel = FindObjectOfType<InventoryPanel>();
    }


    [SerializeField]
    public Inventory PlayerInventory { get; private set; } = new Inventory()
    {
        MaxCapacity = 9
    };


    [SerializeField] public InventoryPanel InventoryPanel { get; private set; }




    void Start()
    {


        AddItemToPlayerInventory(ItemList.GetItem<Plant>("Carrot"));
        AddItemToPlayerInventory(ItemList.GetItem<PlaceableItem>("Well"));
        AddItemToPlayerInventory(ItemList.GetItem<WateringCan>("Watering Can"));
        AddItemToPlayerInventory(ItemList.GetItem<Hoe>("Hoe"));



    }



    public void AddItemToPlayerInventory(Item item)
    {
        PlayerInventory.AddItem(item);
        item.OnItemDestroyed += () => InventoryPanel.Refresh();
        InventoryPanel.Refresh();
    }




    void AddItemsToPlayerInventory(List<Item> items)
    {
        foreach (var item in items)
        {
            PlayerInventory.AddItem(item);
            item.OnItemDestroyed += () => InventoryPanel.Refresh();
        }
        InventoryPanel.Refresh();
    }

}



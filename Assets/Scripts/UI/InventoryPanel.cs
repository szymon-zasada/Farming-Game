using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{

    [SerializeField] public RectTransform Background;
    [SerializeField] public RectTransform Container;

    [SerializeField] private List<GameObject> _itemSlots = new List<GameObject>();


    public void Start()
    {


    }


    public void AddItemSlot(Item item)
    {
        GameObject itemSlot = Instantiate(Resources.Load("Prefabs/UI/ItemSlot"), Container) as GameObject;
        _itemSlots.Add(itemSlot);
        itemSlot.GetComponent<ItemSlot>().SetItem(item);
    }


    public void Refresh()
    {
        // foreach (var itemSlot in _itemSlots)
        // {
        //     Destroy(itemSlot);
        // }
        // _itemSlots.Clear();
        // foreach (var item in InventoryManager.Instance.PlayerInventory.Items)
        // {
        //     AddItemSlot(item);
        // }


        foreach (Item playerInventoryItem in InventoryManager.Instance.PlayerInventory.Items)
        {
            // Check if the item is already in an item slot
            bool itemInSlot = false;
            foreach (GameObject itemSlotObject in _itemSlots)
            {
                ItemSlot itemSlot = itemSlotObject.GetComponent<ItemSlot>();
                if (itemSlot.Item.Name == playerInventoryItem.Name)
                {
                    itemSlot.RefreshSlot();
                    itemInSlot = true;
                    break;
                }
            }
            // If the item is not in an item slot, add it to the list
            if (!itemInSlot)
            {
                AddItemSlot(playerInventoryItem);
            }
        }

        List<Item> itemsInSlots = _itemSlots.ConvertAll(x => x.GetComponent<ItemSlot>().Item);

        foreach (Item item in itemsInSlots)
        {
            if (!InventoryManager.Instance.PlayerInventory.Items.Contains(item))
            {
                ItemSlot itemSlot = _itemSlots[itemsInSlots.IndexOf(item)].GetComponent<ItemSlot>();
                _itemSlots.Remove(_itemSlots[itemsInSlots.IndexOf(item)]);
                Destroy(itemSlot.gameObject);
            }
        }


    }


    public void Update()
    {
        if (_itemSlots.Count > 0)
        {
            Background.sizeDelta = new Vector2(Container.sizeDelta.x + 20, 140);
        }
        else
        {
            Background.sizeDelta = new Vector2(0, 0);
        }
    }
}

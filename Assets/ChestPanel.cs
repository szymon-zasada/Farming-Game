using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPanel : MonoBehaviour
{
    [SerializeField] GameObject _content;
    [SerializeField] List<ItemSlot> _itemSlots = new List<ItemSlot>();


    void Start()
    {
        foreach (ItemSlot itemSlot in _content.GetComponentsInChildren<ItemSlot>())
        {
            _itemSlots.Add(itemSlot);
            itemSlot.Clear();
        }
    }

    public void SetItemSlots(List<Item> items)
    {
        foreach (ItemSlot itemSlot in _itemSlots)
        {
            itemSlot.Clear();
        }

        for (int i = 0; i < items.Count; i++)
        {
            _itemSlots[i].SetItem(items[i]);
        }
    }


    public void Refresh()
    {
        foreach (ItemSlot itemSlot in _itemSlots)
        {
            itemSlot.RefreshSlot();
        }
    }


}

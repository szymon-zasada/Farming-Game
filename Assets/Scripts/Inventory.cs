using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{   
    public List<Item> Items { get; private set; } = new List<Item>();



    public void AddItem(Item item)
    {
        Items.Add(item);
        item.OnItemDestroyed += () => DestroyItem(item);
    }

    public void DestroyItem(Item item)
    {
        Debug.Log("Destroying item");
        Items.Remove(item);
    }



}

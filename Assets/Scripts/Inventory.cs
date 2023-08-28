using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class Inventory
{
    public List<Item> Items { get; private set; } = new List<Item>();



    public void AddItem(Item item)
    {
        if (item is IStackable stackableItem)
        {
            Item existingItem = Items.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem is IStackable existingStackableItem)
            {
                Debug.Log("ExistingStackable: " + existingStackableItem.Quantity);
                Debug.Log("Stackable: " + stackableItem.Quantity);
                existingStackableItem.Quantity += stackableItem.Quantity;
            }
            else if (existingItem != null)
            {
                // Item with the same name exists but is not stackable
                Debug.LogWarning($"Cannot stack {item.Name} with existing non-stackable item.");
            }

            else
            {
                Items.Add((Item)stackableItem);
                item.OnItemDestroyed += () => DestroyItem(item);
            }
        }
        else
        {
            Items.Add(item);
            item.OnItemDestroyed += () => DestroyItem(item);
        }
    }

    public void DestroyItem(Item item)
    {
        if (item is IStackable stackableItem)
        {
            stackableItem.Quantity--;
            if (stackableItem.Quantity <= 0)
                Items.Remove(item);
        }
        else
            Items.Remove(item);

        Debug.Log("Item destroyed in inventory");
    }



}

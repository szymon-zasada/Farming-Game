using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<Item> Items { get; private set; } = new List<Item>();



    public void AddItem(Item item)
    {
        if (item is IStackable addedItem)
        {
            foreach (var i in Items)
            {
                if (i.Name == item.Name)
                {
                    if (i is IStackable stackableitem)
                    {
                        stackableitem.Quantity += addedItem.Quantity;
                        return;
                    }
                }
            }
        }
        Items.Add(item);
        item.OnItemDestroyed += () => DestroyItem(item);
    }

    public void DestroyItem(Item item)
    {
        Items.Remove(item);
    }



}

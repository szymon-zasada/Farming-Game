using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

    public event Action OnItemDestroyed;
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public Sprite Icon { get; protected set; }
    public int Uses { get; protected set; }
    public int MaxUses { get; protected set; }



    public Item()
    {

    }


    public Item(string name, string description, string iconName, int uses)
    {
        Name = name;
        Description = description;
        Icon = Resources.Load<Sprite>("Textures/Items/" + iconName);
        Uses = uses;
        MaxUses = uses;
    }

    public virtual void Use()
    {
        InventoryManager.Instance.InventoryPanel.Refresh();
    }

    public virtual void Destroy()
    {
        OnItemDestroyed?.Invoke();
    }


    
}


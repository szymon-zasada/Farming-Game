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
    public int MaxQuantity { get; protected set; }
    public int Quantity { get; protected set; }
    public int Uses { get; protected set; }



    public Item()
    {

    }


    public Item(string name, string description, string iconName, int uses, int maxQuantity = 1, int quantity = 1)
    {
        Name = name;
        Description = description;
        Icon = Resources.Load<Sprite>("Textures/Items/" + iconName);
        MaxQuantity = maxQuantity;
        Quantity = quantity;
        Uses = uses;
    }

    public virtual void Use()
    {

    }

    public virtual void Destroy()
    {
        OnItemDestroyed?.Invoke();
    }


    
}


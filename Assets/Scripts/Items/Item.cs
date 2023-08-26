using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Item
{

 
 
    public event Action OnItemDestroyed;
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public string IconName { get; protected set; }



    public Item()
    {

    }

    
    public Item(string name, string description)
    {
        Name = name;
        Description = description;
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


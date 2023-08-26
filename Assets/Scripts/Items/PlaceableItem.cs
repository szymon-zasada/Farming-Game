using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

[Serializable]
public class PlaceableItem : Item, IPlaceable
{
    public string EntityName { get; set; }

    public void Place()
    {
        Debug.Log("Placing " + Name);
        if (InteractionManager.Instance.SelectedTile is not ICanPlaceOn tile)
        {
            InteractionManager.Instance.ResetSelectedItem();
            throw new System.InvalidOperationException("You can't place that here!");
        }
        else
        {
            tile.Place(this);
            Destroy();
        }
    }

    [JsonConstructor]
    public PlaceableItem(string name, string description, string entityName)
    {
        Name = name;
        Description = description;
        EntityName = entityName;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Grass : Tile, IFertilizable, ICanPlaceOn
{
    public Entity Entity { get; set; }
    public void FertilizeTile()
    {
        //swap to farmland
        GridManager.Instance.ChangeTile<Farmland>(this);
    }

    public void Place(Item item)
    {

        if(item != null)
            throw new System.InvalidOperationException("There is already an entity here!");

        if(item is not IPlaceable placeableItem)
            throw new System.InvalidOperationException("You can't place that here!");

        
        Type entityType = Type.GetType(placeableItem.EntityName);
        Debug.Log(entityType);

        
    }
}

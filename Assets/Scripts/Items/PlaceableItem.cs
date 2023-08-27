using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Reflection;

[Serializable]
public class PlaceableItem : Item, IPlaceable
{
    public string EntityName { get; set; }

    public void Place(ICanPlaceOn tile)
    {
        if (tile.Entity != null)
        {
            InteractionManager.Instance.ResetSelectedItem();
            throw new System.InvalidOperationException("There is already an entity here!");
        }

        Type entityType = Type.GetType(EntityName);

        if (entityType == null)
        {
            InteractionManager.Instance.ResetSelectedItem();
            throw new System.InvalidOperationException("Entity type not found!");
        }



        MethodInfo spawnEntityMethod = typeof(Entity).GetMethod("SpawnEntity");
        MethodInfo genericSpawnEntityMethod = spawnEntityMethod.MakeGenericMethod(entityType);
        tile.Entity = (Entity)genericSpawnEntityMethod.Invoke(null, new object[] { tile });
    }


    public override void Use()
    {
        if (InteractionManager.Instance.SelectedTile is not ISolidTile)
        {
            InteractionManager.Instance.ResetSelectedItem();
            throw new System.InvalidOperationException("This is not a solid block!");
        }

        if (InteractionManager.Instance.SelectedTile is not ICanPlaceOn tile)
        {
            InteractionManager.Instance.ResetSelectedItem();
            throw new System.InvalidOperationException("You can't place that here!");
        }

        Place(tile);
        Destroy();
    }

    [JsonConstructor]
    public PlaceableItem(string name, string description, string entityName)
    {
        Name = name;
        Description = description;
        EntityName = entityName;
    }
}

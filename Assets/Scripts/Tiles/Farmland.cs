using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmland : Tile, IHasInventory, IFertile
{
    public Inventory Inventory { get; set; } = new Inventory();
    public GrowingEntity GrowingEntity { get; set; }

    public override void Start()
    {
        Inventory = new Inventory();
    }

    public void AddItemToInventory(Item item)
    {
        Debug.Log($"Added {item.Name} to tile inventory: {this.Id}");
        Inventory.AddItem(item);
    }

    public void AddItemsToInventory(List<Item> items)
    {
        foreach (var item in items)
        {
            Inventory.AddItem(item);
        }
    }

    public void Plant(Item item)
    {
        if (item is IPlantable)
        {
            //Inventory.DestroyItem(item);
            GameObject growingEntityGameObject = Instantiate(Resources.Load("Prefabs/Carrot"), transform.position, Quaternion.identity) as GameObject;
            growingEntityGameObject.transform.parent = transform;
            GrowingEntity = growingEntityGameObject.AddComponent<GrowingEntity>();
            GrowingEntity.SetInfo(item as IPlantable);
        }
    }
}

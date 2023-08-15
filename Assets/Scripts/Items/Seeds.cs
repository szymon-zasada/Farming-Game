using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : Item, IPlantable
{
    public float GrowthTime { get; set; }
    public Item RewardItem { get; set; }
    

    public Seeds()
    {
        Name = "Carrot seeds";
        Description = "A bag of seeds";
        Icon = Resources.Load<Sprite>("Textures/Items/download");
        MaxQuantity = 99;
        Quantity = 1;
        Uses = 1;

        GrowthTime = 1000f;
        
    }

    public override void Use()
    {
        if(InteractionManager.Instance.SelectedItem != this)
            return;

        if(InteractionManager.Instance.SelectedTile is IFertile tile and IHasInventory tileInventory)
        {
            tileInventory.AddItemToInventory(this);
            tile.Plant(this);
            Uses--;
            Debug.Log($"Seeds used! {Uses} uses left");

            if(Uses <= 0)
            {
                Debug.Log("Seeds used up!");
              //  Destroy();
            }
        }

        else
        {
            Debug.Log("You can't use that here!");
        }
    }

}

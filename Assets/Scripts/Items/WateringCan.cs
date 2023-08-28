using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class WateringCan : Item, IWaterContainer, IMultipleUses
{
    public int Uses { get; set; }
    public int MaxUses { get; set; }


    [JsonConstructor]
    public WateringCan(string name, string description, int uses = 4, int maxUses = 4)
    {
        Name = name;
        Description = description;
        Uses = uses;
        MaxUses = maxUses;
    }



    public override void Use()
    {
        if (InteractionManager.Instance.SelectedItem != this)
            return;

        if (InteractionManager.Instance.SelectedTile is ICanPlaceOn canPlaceOnTile)
            if (canPlaceOnTile.Entity is IWaterSource)
            {
                Fill();
                return;
            }


        if (InteractionManager.Instance.SelectedTile is not IFertile tile)
            throw new InvalidOperationException("You can't use that here!");

        if(tile.GrowingEntity.IsGrown)
            throw new InvalidOperationException("This plant is already grown!");

        if(tile.GrowingEntity.WaterLevel < tile.GrowingEntity.MaxWaterLevel)
        {
            int waterNeeded = (int)(tile.GrowingEntity.MaxWaterLevel - tile.GrowingEntity.WaterLevel);
            int waterAvailable = Uses;
            if(waterNeeded < 1)
                waterNeeded = 1;

            if (waterAvailable < waterNeeded)
                waterNeeded = waterAvailable;
            tile.GrowingEntity.WaterTile(waterNeeded);

            Uses -= waterNeeded;
            InventoryManager.Instance.InventoryPanel.Refresh();
            InteractionManager.Instance.ResetSelectedItem();
        }
        else
            Debug.Log("This plant doesn't need any more water!");
        

        if (Uses <= 0)
        {
            Uses = 0;
            Debug.Log("Your watering can is empty!");
        }
    }


    public void Fill()
    {
        Uses = MaxUses;
        InteractionManager.Instance.ResetSelectedItem();
        InventoryManager.Instance.InventoryPanel.Refresh();
    }
}

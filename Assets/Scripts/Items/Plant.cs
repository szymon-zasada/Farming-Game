using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Plant : Item, IPlantable, IProcessable, IStackable
{
    public int MaxQuantity { get; set; }
    public int Quantity { get; set; }
    public float GrowthTime { get; set; }
    public Item RewardItem { get; set; }
    public int MaxReward { get; set; }
    public int MinReward { get; set; }


    public Plant(string name, string description, int quantity = 1, int maxReward = 3, int minReward = 1, int uses = 1, float growthTime = 1000f)
    {
        Name = name;
        Description = description;
        Icon = Resources.Load<Sprite>("Textures/Items/" + name);
        MaxQuantity = 99;
        Quantity = quantity;
        Uses = uses;
        MaxUses = uses;
        GrowthTime = growthTime;
        MaxReward = maxReward;
        MinReward = minReward;
    }

    public override void Use()
    {
        if (InteractionManager.Instance.SelectedItem != this)
            return;

        if (InteractionManager.Instance.SelectedTile is not IFertile tile)
        {
            InteractionManager.Instance.ResetSelectedItem();
            throw new InvalidOperationException("Selected tile is not fertile.");
        }



        RewardItem = CreateSingleCopy();

        tile.Plant(this);
        Uses--;

        if (Uses <= 0)
        {
            if (Quantity > 1)
            {
                Quantity--;
                Uses = MaxUses;
            }
            else
                Destroy();
        }
        base.Use();
    }


    Plant CreateSingleCopy()
    {
        if(this.RewardItem != null)
            return this.RewardItem as Plant;
        return new Plant(Name, Description, 1, MaxReward, MinReward, Uses, GrowthTime);
    }






}

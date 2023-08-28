using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Newtonsoft.Json;

[Serializable]
public class Plant : Item, IPlantable, IProcessable, IStackable, ICloneable
{
    public int MaxQuantity { get; set; }
    public int Quantity { get; set; }
    public float GrowthTime { get; set; }
    public float MaxWaterLevel { get; set; }
    public float WaterPerTick { get; set; }
    public Item RewardItem { get; set; }
    public int MaxReward { get; set; }
    public int MinReward { get; set; }


    [JsonConstructor]
    public Plant(string name, string description, int quantity = 1, int maxReward = 3, int minReward = 1, float growthTime = 1000f, float maxWaterLevel = 100f, float waterPerTick = 1f)
    {
        Name = name;
        Description = description;
        MaxQuantity = 99;
        Quantity = quantity;
        GrowthTime = growthTime;
        MaxReward = maxReward;
        MinReward = minReward;
        MaxWaterLevel = maxWaterLevel;
        WaterPerTick = waterPerTick;
    }


    public Plant()
    {
        Name = "Carrot";
        Description = "A carrot.";
        MaxQuantity = 99;
        Quantity = 1;
        GrowthTime = 5f;
        MaxReward = 3;
        MinReward = 1;
        MaxWaterLevel = 5f;
        WaterPerTick = 1f;
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


        tile.Plant((Item)this.Clone());
        Destroy();
        base.Use();
    }


    Plant CreateSingleCopy()
    {
        if (this.RewardItem != null)
        {
            //make a copy of the reward item
            Plant copy = (this.RewardItem as Plant).Clone() as Plant;
            return copy;
        }



        Plant newPlant = new Plant(Name, Description, 1, MaxReward, MinReward, GrowthTime, MaxWaterLevel, WaterPerTick);
        Item item = newPlant as Item;
        item.Icon = Icon;

        return newPlant;
    }


    public void GenerateRandomRewardQuantity()
    {
        IStackable stackableRewardItem = RewardItem as IStackable;
        stackableRewardItem.Quantity = Random.Range(MinReward, MaxReward + 1);
    }


    public object Clone()
    {
        return this.MemberwiseClone();
    }




}

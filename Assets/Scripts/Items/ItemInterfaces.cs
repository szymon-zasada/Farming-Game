using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlantable
{
    public float GrowthTime { get; set; }
    public Item RewardItem { get; set; }
}

public interface IWaterStorage
{
    public void Fill();
}

public interface IProcessable
{
    public int MaxReward { get; set; }
    public int MinReward { get; set; }
}

public interface IStackable
{
    public int MaxQuantity { get; set; }
    public int Quantity { get; set; }
}

public interface IMultipleUses
{
    public int Uses { get; set; }
    public int MaxUses { get; set; }
}


public interface IPlaceable
{
    public string EntityName { get; set; }
    public void Place(ICanPlaceOn tile);
}

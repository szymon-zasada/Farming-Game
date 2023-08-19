using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


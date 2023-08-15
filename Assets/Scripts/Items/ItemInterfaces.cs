using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlantable
{
    public float GrowthTime { get; set; }
    public Item RewardItem { get; set; }

}

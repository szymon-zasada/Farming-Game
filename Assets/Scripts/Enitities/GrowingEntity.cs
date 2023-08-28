using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrowingEntity : Entity, IHarvestable, IWaterable
{
    public float WaterLevel { get; set; }
    public float MaxWaterLevel { get; set; }
    public void WaterTile(float waterAmount) => WaterLevel += waterAmount;

    public IPlantable GrowingItem { get; private set; }
    public float CurrentGrowthTime { get; private set; }
    public bool IsGrown { get; private set; } = false;


    private float _fullyGrownYPosition;
    private bool _isAbleToGrow = false;
    private float _waterPerTick;

    public void SetInfo(IPlantable itemPlantable)
    {
        GrowingItem = itemPlantable;
        CurrentGrowthTime = 0f;
        _isAbleToGrow = true;
        MaxWaterLevel = GrowingItem.MaxWaterLevel;
        WaterLevel = MaxWaterLevel;
        _waterPerTick = GrowingItem.WaterPerTick;
    }

    public override void Start()
    {
        base.Start();
        _fullyGrownYPosition = gameObject.transform.localPosition.y + 0.25f;
    }


    void FixedUpdate()
    {
        if (!_isAbleToGrow)
            return;

        if (IsGrown)
            return;



        float bonusGrowthValue = 2f * (WaterLevel > 0 ? 1 : 0);
        CurrentGrowthTime += Time.fixedDeltaTime * bonusGrowthValue;

        if (WaterLevel > 0)
            WaterLevel -= _waterPerTick * Time.fixedDeltaTime;


        VisualizeGrowth();


        if (CurrentGrowthTime >= GrowingItem.GrowthTime)
        {
            IsGrown = true;
        }
    }


    void VisualizeGrowth()
    {
        float growthPercentage = CurrentGrowthTime / GrowingItem.GrowthTime;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
                                                        Mathf.Clamp(_fullyGrownYPosition * growthPercentage, 0.01f, _fullyGrownYPosition),
                                                        gameObject.transform.localPosition.z);
    }



    public void Harvest()
    {
        if (IsGrown)
        {
            if (GrowingItem.RewardItem == null)
                throw new ArgumentException("Reward item is null.");



            GrowingItem.GenerateRandomRewardQuantity();
            Debug.Log("Harvested " + GrowingItem.RewardItem.Name + " x" + (GrowingItem.RewardItem as IStackable).Quantity);
            InventoryManager.Instance.AddItemToPlayerInventory(GrowingItem.RewardItem);
            InteractionManager.Instance.ResetSelectedItem();
            Destroy(gameObject);
        }
    }
}

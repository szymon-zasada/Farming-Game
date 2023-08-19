using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingEntity : Entity, IHarvestable
{
   // public Action OnHarvest { get; set; }
    public IPlantable GrowingItem { get; private set; }
    public float CurrentGrowthTime { get; private set; }
    public bool IsGrown { get; private set; } = false;

    private float _fullyGrownYPosition;

    private bool _isAbleToGrow = false;

    public void SetInfo(IPlantable itemPlantable)
    {
        GrowingItem = itemPlantable;
        CurrentGrowthTime = 0f;
        _isAbleToGrow = true;
    }

    public override void Start()
    {
        base.Start();
        _fullyGrownYPosition = EntityPrefab.transform.localPosition.y + Vector3.up.y;
    }


    void FixedUpdate()
    {
        if(!_isAbleToGrow)
            return;

        if(IsGrown)
            return;
        

        CurrentGrowthTime += Time.fixedDeltaTime;
        float growthPercentage = CurrentGrowthTime / GrowingItem.GrowthTime;
        EntityPrefab.transform.localPosition = new Vector3(EntityPrefab.transform.localPosition.x, Mathf.Clamp(_fullyGrownYPosition*growthPercentage, 0.01f, _fullyGrownYPosition), EntityPrefab.transform.localPosition.z);

        if (CurrentGrowthTime >= GrowingItem.GrowthTime)
        {
            IsGrown = true;
        }
    }



    public void Harvest()
    {
        if (IsGrown)
        {
            InventoryManager.Instance.AddItemToPlayerInventory(GrowingItem.RewardItem);
            Debug.Log($"Harvested {GrowingItem.RewardItem.Name}");
            Destroy(EntityPrefab);               
        }
    }
}

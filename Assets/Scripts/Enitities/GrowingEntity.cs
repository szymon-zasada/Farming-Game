using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingEntity : Entity, IHarvestable
{
    public Action OnHarvest { get; set; }
    public Item RewardItem { get; private set; }
    public float GrowthTime { get; private set; }
    public float CurrentGrowthTime { get; private set; }
    public bool IsGrown { get; private set; } = false;

    private float _fullyGrownYPosition;

    private bool _isAbleToGrow = false;

    public void SetInfo(IPlantable itemPlantable)
    {
        RewardItem = itemPlantable.RewardItem;
        GrowthTime = itemPlantable.GrowthTime;
        _isAbleToGrow = true;
    }

    public override void Start()
    {
        base.Start();
        _fullyGrownYPosition = EntityPrefab.transform.localPosition.y + Vector3.up.y;
    }


    void Update()
    {
        if(!_isAbleToGrow)
            return;
        

        CurrentGrowthTime += Time.fixedDeltaTime;
        float growthPercentage = CurrentGrowthTime / GrowthTime;
        EntityPrefab.transform.localPosition = new Vector3(EntityPrefab.transform.localPosition.x, Mathf.Clamp(_fullyGrownYPosition*growthPercentage, 0.01f, _fullyGrownYPosition), EntityPrefab.transform.localPosition.z);

        if (CurrentGrowthTime >= GrowthTime)
        {
            IsGrown = true;
        }
    }



    public void Harvest()
    {
        if (IsGrown)
        {
            Debug.Log("Harvesting");
            InventoryManager.Instance.PlayerInventory.AddItem(RewardItem);
            Destroy(EntityPrefab);
            OnHarvest?.Invoke();

            
            
        }
    }
}

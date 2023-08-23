using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        _fullyGrownYPosition = EntityPrefab.transform.localPosition.y + 0.25f;
    }


    void FixedUpdate()
    {
        if (!_isAbleToGrow)
            return;

        if (IsGrown)
            return;


        CurrentGrowthTime += Time.fixedDeltaTime;
        float growthPercentage = CurrentGrowthTime / GrowingItem.GrowthTime;
        EntityPrefab.transform.localPosition = new Vector3(EntityPrefab.transform.localPosition.x, Mathf.Clamp(_fullyGrownYPosition * growthPercentage, 0.01f, _fullyGrownYPosition), EntityPrefab.transform.localPosition.z);

        if (CurrentGrowthTime >= GrowingItem.GrowthTime)
        {
            IsGrown = true;
        }
    }



    public void Harvest()
    {
        if (IsGrown)
        {
            if (GrowingItem.RewardItem == null)
                throw new ArgumentException("Reward item is null.");

            
    

            InventoryManager.Instance.AddItemToPlayerInventory(GrowingItem.RewardItem);
            InteractionManager.Instance.ResetSelectedItem();
            Destroy(EntityPrefab);
        }
    }
}

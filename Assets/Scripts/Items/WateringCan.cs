using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WateringCan : Item, IWaterStorage, IMultipleUses
{
    public int Uses { get; set; }
    public int MaxUses { get; set; }

    
    public WateringCan()
    {

    }


    public override void Use()
    {
        if (InteractionManager.Instance.SelectedItem != this)
            return;

        if (InteractionManager.Instance.SelectedTile is not IWaterable tile)
        {
            throw new InvalidOperationException("You can't use that here!");
        }

        tile.WaterTile();
        Uses--;

        if (Uses <= 0)
        {
            Uses = 0;
            Debug.Log("Your watering can is empty!");
        }
    }


    public void Fill()
    {
        if (InteractionManager.Instance.SelectedItem != this)
            return;
        

        if (InteractionManager.Instance.SelectedTile is ISolidBlock solidBlock and ICanPlaceOn tile)
        {
            if (tile.Entity is IWaterSource)
            {
                Uses = 4;
            }

            else
                Debug.Log("You can't fill that here!");
        }

        else
            Debug.Log("You can't fill that here!");
    }
}

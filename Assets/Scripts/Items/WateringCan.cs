using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Item, IWaterStorage
{
    public WateringCan()
    {
        Name = "Watering Can";
        Description = "A watering can for watering plants.";
        Icon = Resources.Load<Sprite>("Textures/Items/wateringCan");
        Uses = 4;
    }


    public override void Use()
    {
        if(InteractionManager.Instance.SelectedItem != this)
        {
            Debug.Log("You need to select the watering can first!");
            return;
        }

        if(InteractionManager.Instance.SelectedTile is IWaterable tile)
        {
            tile.WaterTile();
            Uses--;

            if(Uses <= 0)
            {
                Uses = 0;
                Debug.Log("Watering can empty!");
            }
        }

        else
        {
            Debug.Log("You can't use that here!");
        }
    }


    public void Fill()
    {
        if(InteractionManager.Instance.SelectedItem != this)
        {
            Debug.Log("You need to select the watering can first!");
            return;
        }

        if(InteractionManager.Instance.SelectedTile is ISolidBlock solidblock and ICanBuildOn tile)
        {
            if(tile.Entity is IWaterSource)
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

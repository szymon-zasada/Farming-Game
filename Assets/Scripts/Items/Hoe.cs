using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hoe : Item
{

    public Hoe()
    {
        Name = "Hoe";
        Description = "A hoe for tilling the land";
        Icon = Resources.Load<Sprite>("Textures/Items/Hoe");
        MaxQuantity = 1;
        Quantity = 1;
        Uses = 1;
    }


    public override void Use()
    {
        if(InteractionManager.Instance.SelectedItem != this)
        {
            Debug.Log("You need to select the hoe first!");
            return;
        }

        if(InteractionManager.Instance.SelectedTile is IFertilizable tile)
        {
            tile.FertilizeTile();
            Uses--;
            Debug.Log($"Hoe used! {Uses} uses left");

            if(Uses <= 0)
            {
                Debug.Log("Hoe broke!");
                Destroy();
            }
        }

        else
        {
            Debug.Log("You can't use that here!");
        }
    }
}
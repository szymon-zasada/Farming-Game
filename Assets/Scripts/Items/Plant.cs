using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Item, IPlantable, IProcessable, IStackable
{
    public int MaxQuantity { get; set; }
    public int Quantity { get; set; }
    public float GrowthTime { get; set; }
    public Item RewardItem { get; set; }
    public int MaxReward { get; set; }
    public int MinReward { get; set; }



    public Plant()
    {
        Name = "Carrot";
        Description = "";
        Icon = Resources.Load<Sprite>("Textures/Items/carrot");
        MaxQuantity = 99;
        Quantity = 1;
        Uses = 1;
        MaxReward = 3;
        MinReward = 1;
        RewardItem = new Plant("Carrot", "", Random.Range(MinReward, MaxReward));

        GrowthTime = 8f;
    }

    public Plant(string name, string description, int quantity = 1, int maxReward = 3, int minReward = 1, int uses = 1, float growthTime = 1000f)
    {
        Name = name;
        Description = description;
        Icon = Resources.Load<Sprite>("Textures/Items/" + name);
        MaxQuantity = 99;
        Quantity = quantity;
        Uses = uses;
        MaxUses = uses;
        GrowthTime = growthTime;
        MaxReward = maxReward;
        MinReward = minReward;
    }

    public override void Use()
    {
        if (InteractionManager.Instance.SelectedItem != this)
            return;

        if (InteractionManager.Instance.SelectedTile is IFertile tile)
        {
            RewardItem = CreateSingleCopy();
            tile.Plant(this);
            Uses--;

            if (Uses <= 0)
            {
                if(Quantity > 1)
                {
                    Quantity--;
                    Uses = MaxUses;
                }
                else
                {
                    Destroy();
                }
            }
        }

        else
        {
            Debug.Log("You can't use that here!");
        }


        base.Use();
    }


    Plant CreateSingleCopy()
    {
        return new Plant(Name, Description, Random.Range(MinReward,MaxReward), MaxReward, MinReward, Uses, GrowthTime);
    }


}

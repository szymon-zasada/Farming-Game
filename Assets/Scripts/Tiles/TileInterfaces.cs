using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFertilizable
{
    void FertilizeTile();
}

public interface IFertile
{
    public GrowingEntity GrowingEntity { get; set; }
    void Plant(Item item);
    
}

public interface ISolidTile
{


}

public interface ICanPlaceOn
{
    public Entity Entity { get; set; }
}
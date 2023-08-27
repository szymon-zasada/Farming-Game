using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Grass : Tile, IFertilizable, ICanPlaceOn, ISolidTile
{
    public Entity Entity { get; set; }
    public void FertilizeTile()
    {
        //swap to farmland
        GridManager.Instance.ChangeTile<Farmland>(this);
    }
}

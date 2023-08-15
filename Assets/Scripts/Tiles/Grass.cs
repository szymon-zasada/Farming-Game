using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Tile, IFertilizable
{
    public void FertilizeTile()
    {
        //swap to farmland
        GridManager.Instance.ChangeTile<Farmland>(this);
    }
}

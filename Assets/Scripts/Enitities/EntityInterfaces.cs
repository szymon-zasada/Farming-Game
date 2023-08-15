
using System;

public interface IHarvestable
{
    public Action OnHarvest { get; set; }
    void Harvest();
}

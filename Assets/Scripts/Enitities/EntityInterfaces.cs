
using System;

public interface IHarvestable
{
    void Harvest();
}

public interface IWaterSource
{
    void Fill();
}

public interface IWaterable
{
    public float WaterLevel { get; set; }
    public float MaxWaterLevel { get; set; }
    public void WaterTile(float waterAmount);

}

public interface IStorage
{
    public Inventory Inventory { get; set; }
}
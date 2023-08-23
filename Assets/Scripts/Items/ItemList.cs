using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class ItemList
{
    public static List<Plant> PlantList { get; private set; } = new List<Plant>();

    public static void LoadItems()
    {
        PlantList = Json.LoadFrom<List<Plant>>(DataPaths.PLANTS_PATH);
    }

    public static T GetItem<T>(string name) where T : Item
    {
        return PlantList.FirstOrDefault(i => i.Name == name) as T;
    }


}

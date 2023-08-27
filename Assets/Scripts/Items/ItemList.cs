using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using System.Reflection;


public class ItemList
{
    public static List<Item> List { get; private set; } = new List<Item>();

    public static void LoadItems()
    {
        string json = File.ReadAllText(DataPaths.ITEMS_PATH);
        List = JsonConvert.DeserializeObject<List<Item>>(json, new ItemConverter());
        LoadIcons();
    }

    public static T GetItem<T>(string name) where T : Item
    {
        return List.FirstOrDefault(i => i.Name == name) as T;
    }

    public static void LoadIcons()
    {
        foreach (var item in List)
        {
            string searchName = item.Name;
            if(searchName.Contains(" "))
                searchName = searchName.Replace(" ", "");
        
            Sprite sprite = Resources.Load<Sprite>("Textures/Items/" + searchName);
            if (sprite != null)
                item.Icon = sprite;
            else
            {
                item.Icon = Resources.Load<Sprite>("Textures/Items/Default");
                throw new System.InvalidOperationException("Item icon not found!");
            }


        }
    }
}



public class ItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Item).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject itemObject = JObject.Load(reader);
        
        Type type = Type.GetType(itemObject["Type"].ToString());
        if (type == null)
            throw new InvalidOperationException("Invalid item type!");

        itemObject.Remove("Type");
        return itemObject.ToObject(type);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}


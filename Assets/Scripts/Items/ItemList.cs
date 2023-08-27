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
            item.Icon = Resources.Load<Sprite>("Textures/Items/" + item.Name);
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
        Type type = GetTypeFromProperties(itemObject);
        if (type == null)
            throw new InvalidOperationException("Invalid item type!");

        return itemObject.ToObject(type);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    private Type GetTypeFromProperties(JObject itemObject)
    {
        Type[] types = new[] { typeof(Plant), typeof(PlaceableItem) };
        

        foreach (var type in types)
        {


            var itemObjectProperties = itemObject.Properties().Select(p => p.Name);

            var ignoredTypeProperties = new[] { "Icon" };
            var typeProperties = type.GetProperties().Select(p => p.Name).Except(ignoredTypeProperties);


            if (itemObjectProperties.All(p => typeProperties.Contains(p)))
                return type;
        }

        return null;
    }
}


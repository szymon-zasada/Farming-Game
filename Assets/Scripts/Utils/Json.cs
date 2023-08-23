using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Json
{
    public static void SaveTo<T>(T obj, string path)
    {
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        System.IO.File.WriteAllText(path, json);
    }

    public static void AddToFile<T>(T obj, string path)
    {
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        System.IO.File.AppendAllText(path, json);
    }


    public static T LoadFrom<T>(string path)
    {
        string json = System.IO.File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
    }
}

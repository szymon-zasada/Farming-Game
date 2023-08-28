using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

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


    public static List<T> LoadFrom<T>(string path)
    {
        var textAsset = Resources.Load(path) as TextAsset;
        string json = textAsset.text;
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}

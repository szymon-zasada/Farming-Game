using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public int Id { get; set; }
    public Vector3Int GridPosition { get; set; }


    public virtual void Start()
    {

    }

    void Update()
    {

    }





    public static Tile CreateNewTile(string type, Vector3Int gridPosition, Vector3 worldPosition, int tileId)
    {
        if (type == "Grass")
        {
            var gameObj = Instantiate(Resources.Load("Prefabs/Tileprefab"), worldPosition, Quaternion.identity) as GameObject;
            // var gameObj = Instantiate(Resources.Load("Models/baseGrass"), worldPosition, Quaternion.identity) as GameObject;
            gameObj.transform.parent = GameObject.Find("GameGrid").transform;

            gameObj.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Textures/Grass") as Texture);

            gameObj.AddComponent<Grass>();
            Destroy(gameObj.GetComponent<Tile>());
            var grass = gameObj.GetComponent<Grass>();
            grass.Id = tileId;
            grass.GridPosition = gridPosition;

            return grass;
        }
        else
        {
            return null;
        }
    }

    public static T ChangeTileType<T>(Tile tile)
    {
        Type type = typeof(T);
        tile.gameObject.AddComponent(type);
        var newTile = tile.gameObject.GetComponent(type);
        PropertyInfo[] newProperties = type.GetProperties();
        PropertyInfo[] properties = tile.GetType().GetProperties();

        foreach (var newProperty in newProperties)
        {
            foreach (var property in properties)
            {
                if (newProperty.Name == property.Name && newProperty.PropertyType == property.PropertyType && newProperty.CanWrite)
                {
                    newProperty.SetValue(newTile, property.GetValue(tile));
                }
            }
        }
        var gameObj = tile.gameObject;
        Destroy(tile);

        if(gameObj.transform.childCount > 0)
            Destroy(gameObj.transform.GetChild(0).gameObject);
        
        gameObj.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Textures/" + typeof(T)) as Texture);
        return gameObj.GetComponent<T>();
    }
}



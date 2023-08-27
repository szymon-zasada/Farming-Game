using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Reflection;

public class Entity : MonoBehaviour
{
    public string Name { get; protected set; }

    public virtual void Start()
    {
        // gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Entities/" + Name);
    }

    public static T SpawnEntity<T>(Tile tile) where T : Entity
    {
        var prefab = Resources.Load("Prefabs/" + typeof(T).ToString()) as GameObject;
        var position = tile.transform.position + prefab.transform.position;
        var gameObj = Instantiate(prefab, position, Quaternion.identity*prefab.transform.rotation) as GameObject;
        gameObj.transform.parent = tile.transform;
        gameObj.AddComponent<T>();
        return gameObj.GetComponent<T>();
    }


}

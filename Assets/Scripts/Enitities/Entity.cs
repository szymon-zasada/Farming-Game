using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string Name { get; protected set; }
    public GameObject EntityPrefab { get; protected set; }

    public virtual void Start()
    {
        EntityPrefab = gameObject;
       // gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Entities/" + Name);
    }


}

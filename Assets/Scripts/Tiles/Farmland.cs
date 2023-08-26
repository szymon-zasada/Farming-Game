using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmland : Tile, IFertile
{
    public GrowingEntity GrowingEntity { get; set; }

    public override void Start()
    {

    }

    public void Plant(Item item)
    {
        if (item is not IPlantable)
            throw new ArgumentException("Item is not plantable.");

        if (GrowingEntity != null)
            throw new ArgumentException("There is already a growing entity on this farmland.");


        GrowingEntity = GrowingEntity.SpawnEntity<GrowingEntity>(this);


        // GameObject growingEntityGameObject = Instantiate(Resources.Load("Prefabs/"+typeof(GrowingEntity).ToString()), transform.position, Quaternion.identity) as GameObject;


        for (int i = 4; i > 0; i--)
        {
            CreateVisualItems(item, GrowingEntity.gameObject, 0.65f, 0.1f, 0.55f);
        }

        GrowingEntity.SetInfo(item as IPlantable);
    }



    void CreateVisualItems(Item item, GameObject growingEntityGameObject, float randomOffset, float randomOffsetY, float minDistance)
    {
        Vector3 randomOffsetVector = new Vector3(UnityEngine.Random.Range(-randomOffset, randomOffset), UnityEngine.Random.Range(-randomOffsetY, randomOffsetY), UnityEngine.Random.Range(-randomOffset, randomOffset));

        foreach (Transform child in growingEntityGameObject.transform)
        {
            if (Vector3.Distance(child.position, randomOffsetVector) < minDistance)
            {
                CreateVisualItems(item, growingEntityGameObject, randomOffset, randomOffsetY, minDistance);
            }
        }

        GameObject plant = Instantiate(Resources.Load("Prefabs/Plants/" + item.Name)) as GameObject;
        plant.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Textures/Entities/" + item.Name + "_tex"));
        float tempPositionFromPrefab = plant.transform.position.y;
        Vector3 plantPosition = new Vector3(growingEntityGameObject.transform.position.x, plant.transform.position.y, growingEntityGameObject.transform.position.z);



        plant.transform.position = plantPosition + randomOffsetVector;
        plant.transform.parent = growingEntityGameObject.transform;
        plant.transform.localPosition = new Vector3(plant.transform.localPosition.x, tempPositionFromPrefab + randomOffsetVector.y, plant.transform.localPosition.z);

    }
}

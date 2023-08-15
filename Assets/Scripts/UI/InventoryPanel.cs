using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{

    [SerializeField] public RectTransform Background;
    [SerializeField] public RectTransform Container;

    [SerializeField] private List<GameObject> _itemSlots = new List<GameObject>();


    public void Start()
    {


    }


    public void AddItemSlot(Item item)
    {
        GameObject itemSlot = Instantiate(Resources.Load("Prefabs/UI/ItemSlot"), Container) as GameObject;
        _itemSlots.Add(itemSlot);
        itemSlot.GetComponent<ItemSlot>().SetItem(item);
    }

    public void Refresh()
    {
        foreach (var itemSlot in _itemSlots)
        {
            Destroy(itemSlot);
        }
        _itemSlots.Clear();
        foreach (var item in InventoryManager.Instance.PlayerInventory.Items)
        {
            AddItemSlot(item);
        }
    }


    public void Update()
    {
        if (_itemSlots.Count > 0)
        {
            Background.sizeDelta = new Vector2(Container.sizeDelta.x + 20, 120);
        }
        else
        {
            Background.sizeDelta = new Vector2(0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Item _item;


    [SerializeField] private Image _image;


    public void SetItem(Item item)
    {
        _item = item;
        _image.sprite = item.Icon;
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on item slot");
        InteractionManager.Instance.SelectedItem = _item;
    }

    public void ActivateButton()
    {
        Debug.Log("Clicked on item slot");
        InteractionManager.Instance.SelectedItem = _item;
    }

 

}

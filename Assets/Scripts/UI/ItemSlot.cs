using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item _item;


    [SerializeField] private Image _image;
    [SerializeField] private Slider _usesSlider;
    [SerializeField] private TMP_Text _quantityText;
    

    [SerializeField] private Button _button;

    public void SetItem(Item item)
    {
        _item = item;
        _image.sprite = item.Icon;
        if (item.MaxUses > 1 && item.Uses != item.MaxUses)
        {
            _usesSlider.gameObject.SetActive(true);
            _usesSlider.maxValue = item.MaxUses;
            _usesSlider.value = item.Uses;
        }
        else
        {
            _usesSlider.gameObject.SetActive(false);
        }

        if (item is IStackable stackableItem)
        {
            if (stackableItem.Quantity > 1)
                _quantityText.text = stackableItem.Quantity.ToString();
            else
                _quantityText.text = "";
        }
        else
        {
            _quantityText.text = "";
        }
    }

    public void ActivateButton()
    {
        if(InteractionManager.Instance.SelectedItem == _item)
        {
            InteractionManager.Instance.ResetSelectedItem();
               return;
        }
        InteractionManager.Instance.SelectedItem = _item;
        
    }



}

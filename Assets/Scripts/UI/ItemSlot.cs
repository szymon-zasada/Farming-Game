using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item Item => _item;


    [SerializeField] private Image _image;
    [SerializeField] private Slider _usesSlider;
    [SerializeField] private TMP_Text _quantityText;

    [SerializeField] private float _desiredUsesValue;


    [SerializeField] private Button _button;

    public void RefreshSlot()
    {
        _image.sprite = _item.Icon;
        if (_item is IMultipleUses multipleUsesItem)
            _desiredUsesValue = multipleUsesItem.Uses;

        if (_item is IStackable stackableItem)
            _quantityText.text = stackableItem.Quantity.ToString();
    }

    public void SetItem(Item item)
    {
        _item = item;
        _image.sprite = item.Icon;
        if (item is IMultipleUses multipleUsesItem)
        {
            if (multipleUsesItem.MaxUses > 1)
            {
                _usesSlider.maxValue = multipleUsesItem.MaxUses;
                _desiredUsesValue = multipleUsesItem.Uses;
                _usesSlider.value = multipleUsesItem.Uses;
                if (multipleUsesItem.Uses != multipleUsesItem.MaxUses)
                {
                    _usesSlider.gameObject.SetActive(true);
                }
                else
                {
                    _usesSlider.gameObject.SetActive(false);
                }

            }
            else
            {
                _usesSlider.gameObject.SetActive(false);
            }
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



    private float _lerpDuration = 2f;
    private float _lerpTimer = 0f;

    void Update()
    {
        IMultipleUses multipleUsesItem = _item as IMultipleUses;

        if (multipleUsesItem == null)
            return;

        if (multipleUsesItem.MaxUses == 1)
            return;
        else
            _usesSlider.gameObject.SetActive(true);

        if (!_usesSlider.IsActive())
            return;

        _desiredUsesValue = multipleUsesItem.Uses;

        if (_usesSlider.value != _desiredUsesValue)
        {
            _lerpTimer += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(_lerpTimer / _lerpDuration);
            _usesSlider.value = Mathf.Lerp(_usesSlider.value, _desiredUsesValue, t);

            if (_usesSlider.value == _desiredUsesValue)
            {
                _lerpTimer = 0f;
            }
        }


        if (multipleUsesItem.MaxUses <= 1 && multipleUsesItem.Uses == multipleUsesItem.MaxUses)
        {
            if (_usesSlider.value == _usesSlider.maxValue)
            {
                _lerpTimer = 0f;
                _usesSlider.gameObject.SetActive(false);
            }
        }

    }


    public void ActivateButton()
    {
        if (InteractionManager.Instance.SelectedItem == _item)
        {
            InteractionManager.Instance.ResetSelectedItem();
            return;
        }
        InteractionManager.Instance.SelectedItem = _item;

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileInteractivePanel : UIPanel
{
    [SerializeField] private Tile _tile;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemQuantity;

    [SerializeField] private Image _rewardItemImage;
    [SerializeField] private TMP_Text _rewardItemQuantity;

    [SerializeField] private Slider _progreessBar;
    [SerializeField] private Button _harvestButton;

    public void SetItem(Tile tile)
    {
        _tile = tile;
        if (tile is IFertile fertileTile)
        {
            if (fertileTile.GrowingEntity != null)
            {
                Item item = fertileTile.GrowingEntity.GrowingItem as Item;
                Debug.Log($"Item: {item.Name}");
                _itemImage.sprite = item.Icon;
                _rewardItemImage.sprite = fertileTile.GrowingEntity.GrowingItem.RewardItem.Icon;
                _progreessBar.maxValue = fertileTile.GrowingEntity.GrowingItem.GrowthTime;
                _progreessBar.value = fertileTile.GrowingEntity.CurrentGrowthTime;
                _itemImage.color = Color.white;
                _rewardItemImage.color = Color.white;
                _itemQuantity.text = "";
                _rewardItemQuantity.text = "";
                _harvestButton.interactable = false;
                _harvestButton.gameObject.SetActive(false);
                IProcessable processable = fertileTile.GrowingEntity.GrowingItem as IProcessable;
                if (processable.MaxReward > 1)
                    _rewardItemQuantity.text = $"{processable.MinReward}-{processable.MaxReward}";

                if (fertileTile.GrowingEntity.CurrentGrowthTime >= fertileTile.GrowingEntity.GrowingItem.GrowthTime)
                {
                    _harvestButton.gameObject.SetActive(true);
                    _harvestButton.interactable = true;
                }

            }
            else
                Clear();
        }
        else
        {
            Debug.Log("Tile is not fertile");
        }
    }

    void FixedUpdate()
    {
        if (_tile is IFertile fertileTile)
        {
            if (fertileTile.GrowingEntity != null)
            {
                _progreessBar.value = fertileTile.GrowingEntity.CurrentGrowthTime;

                if (fertileTile.GrowingEntity.CurrentGrowthTime >= fertileTile.GrowingEntity.GrowingItem.GrowthTime)
                {
                    _harvestButton.gameObject.SetActive(true);
                    _harvestButton.interactable = true;
                }
            }

        }
    }

    public void HarvestButton()
    {
        if (_tile is IFertile fertileTile)
        {
            if (fertileTile.GrowingEntity != null)
            {
                fertileTile.GrowingEntity.Harvest();
                Clear();
            }
        }
    }

    void Clear()
    {
        _progreessBar.maxValue = 1;
        _progreessBar.value = 0;
        _itemImage.sprite = null;
        _itemImage.color = Color.clear;
        _rewardItemImage.color = Color.clear;
        _rewardItemImage.sprite = null;
        _itemQuantity.text = "";
        _rewardItemQuantity.text = "";
        _harvestButton.interactable = false;
        _harvestButton.gameObject.SetActive(false);
    }


}

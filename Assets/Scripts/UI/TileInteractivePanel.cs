using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileInteractivePanel : UIPanel
{
    [SerializeField] private Tile _tile;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemName;

    [SerializeField] private Image _rewardItemImage;

    [SerializeField] private Slider _progreessBar;

    public void SetItem(Tile tile)
    {
        _tile = tile;
        if (tile is IFertile fertileTile && tile is IHasInventory inventoryTile)
        {
            if (inventoryTile.Inventory.Items.Count > 0)
            {
                _itemImage.sprite = inventoryTile.Inventory.Items[0].Icon;
                _itemName.text = inventoryTile.Inventory.Items[0].Name;
                //_rewardItemImage.sprite = fertileTile.GrowingEntity.RewardItem.Icon;
                _progreessBar.maxValue = fertileTile.GrowingEntity.GrowthTime;
                _progreessBar.value = fertileTile.GrowingEntity.CurrentGrowthTime;
            }
            else
            {
                _progreessBar.maxValue = 1;
                _progreessBar.value = 0;

            }
        }
        else
        {
            Debug.Log("Tile is not fertile");
        }
    }

    public void Update()
    {
        if (_tile is IFertile fertileTile && _tile is IHasInventory inventoryTile)
        {
            if (inventoryTile.Inventory.Items.Count > 0)
                _progreessBar.value = fertileTile.GrowingEntity.CurrentGrowthTime;
        }
    }


}

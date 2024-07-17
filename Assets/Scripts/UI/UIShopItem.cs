using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _cost;

    [SerializeField] private BonusUprader _bonusUpgrader;

    private ShopItem _shopItem;

    private void OnEnable()
    {
        if (_shopItem == null)
            return;

        _shopItem.ItemWasUpdated += UpdateBonusInfo;
    }

    private void OnDisable()
    {
        if (_shopItem == null)
            return;

        _shopItem.ItemWasUpdated += UpdateBonusInfo;
    }

    private void UpdateBonusInfo()
    {
        _name.text = _shopItem.BonusData.name;
        _icon.sprite = _shopItem.BonusData.BonusIcon;
        _cost.text = _shopItem.BonusData.BonusInfo.TotalBonusCost().ToString();

        Debug.Log($"{_name.text} : {_cost.text}");
    }

    public void SetupItem(ShopItem item)
    {
        _shopItem = item;
        _shopItem.ItemWasUpdated += UpdateBonusInfo;

        UpdateBonusInfo();
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private Button _upgradeButton;

    private BonusUpgrader _bonusUpgrader;
    private ShopItem _shopItem;

    private void OnEnable()
    {
        if (_shopItem == null)
            return;

        _shopItem.ItemWasUpdated += UpdateBonusInfo;
        _bonusUpgrader.BonusWasUpgraded += OnBonusUpgraded;
    }

    private void OnDisable()
    {
        if (_shopItem == null)
            return;

        _shopItem.ItemWasUpdated -= UpdateBonusInfo;
        _bonusUpgrader.BonusWasUpgraded -= OnBonusUpgraded;
    }

    private void UpdateBonusInfo()
    {
        _name.text = _shopItem.BonusData.name;
        _icon.sprite = _shopItem.BonusData.BonusIcon;
        _cost.text = _shopItem.BonusData.BonusInfo.TotalBonusCost().ToString();

        if (_bonusUpgrader != null)
            _upgradeButton.interactable = _bonusUpgrader.CanUpgradeBonus(_shopItem);
    }

    private void OnBonusUpgraded(ShopItem item)
    {
        if (item == _shopItem)
            UpdateBonusInfo();
    }

    public void SetupItem(ShopItem item)
    {
        _shopItem = item;
        _shopItem.ItemWasUpdated += UpdateBonusInfo;
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);

        UpdateBonusInfo();
    }

    private void OnUpgradeButtonClick()
    {
        if (_bonusUpgrader != null)
            _bonusUpgrader.UpgradeBonus(_shopItem);
    }

    public void SetupBonusUpgrader(BonusUpgrader bonusUpgrader)
    {
        _bonusUpgrader = bonusUpgrader;

        _bonusUpgrader.BonusWasUpgraded += OnBonusUpgraded;

        if (_shopItem != null)
            UpdateBonusInfo();
    }
}
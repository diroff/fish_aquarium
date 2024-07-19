using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _duration;

    [SerializeField] private Button _upgradeButton;

    private BonusUpgrader _bonusUpgrader;
    private ShopItem _shopItem;

    private void OnEnable()
    {
        if (_shopItem != null)
        {
            _shopItem.ItemWasUpdated += UpdateBonusInfo;
            UpdateBonusInfo();
        }

        if (_bonusUpgrader != null)
        {
            _bonusUpgrader.FoodCountWasChanged += CheckUpgradedState;
        }
    }

    private void OnDisable()
    {
        if (_shopItem != null)
        {
            _shopItem.ItemWasUpdated -= UpdateBonusInfo;
        }

        if (_bonusUpgrader != null)
        {
            _bonusUpgrader.FoodCountWasChanged -= CheckUpgradedState;
        }
    }

    private void UpdateBonusInfo()
    {
        _name.text = _shopItem.BonusData.name;
        _icon.sprite = _shopItem.BonusData.BonusIcon;
        _cost.text = _shopItem.BonusData.BonusInfo.TotalBonusCost().ToString();
        _level.text = _shopItem.BonusData.BonusInfo.Level.ToString();
        _duration.text = _shopItem.BonusData.BonusInfo.TotalBonusTime().ToString();

        if (_bonusUpgrader != null)
        {
            _upgradeButton.interactable = _bonusUpgrader.CanUpgradeBonus(_shopItem);
        }
    }

    public void SetupItem(ShopItem item)
    {
        _shopItem = item;
        _shopItem.ItemWasUpdated += UpdateBonusInfo;
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);

        UpdateBonusInfo();
    }

    private void CheckUpgradedState()
    {
        if (_bonusUpgrader != null)
        {
            _upgradeButton.interactable = _bonusUpgrader.CanUpgradeBonus(_shopItem);
        }
    }

    private void OnUpgradeButtonClick()
    {
        if (_bonusUpgrader != null)
        {
            _bonusUpgrader.UpgradeBonus(_shopItem);
        }
    }

    public void SetupBonusUpgrader(BonusUpgrader bonusUpgrader)
    {
        _bonusUpgrader = bonusUpgrader;
        _bonusUpgrader.FoodCountWasChanged += CheckUpgradedState;

        if (_shopItem != null)
        {
            UpdateBonusInfo();
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _level;

    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Image _buttonBlocker;

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
        _icon.sprite = _shopItem.BonusData.BonusIcon;
        _cost.text = _shopItem.BonusData.BonusInfo.TotalBonusCost().ToString();
        _level.text = _shopItem.BonusData.BonusInfo.Level.ToString();

        if (_bonusUpgrader == null)
            return;

        bool canBeUpgraded = _bonusUpgrader.CanUpgradeBonus(_shopItem);

        _upgradeButton.interactable = canBeUpgraded;
        _buttonBlocker.gameObject.SetActive(!canBeUpgraded);
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
        if (_bonusUpgrader == null)
            return;

        bool canBeUpgraded = _bonusUpgrader.CanUpgradeBonus(_shopItem);

        _upgradeButton.interactable = canBeUpgraded;
        _buttonBlocker.gameObject.SetActive(!canBeUpgraded);
    }

    private void OnUpgradeButtonClick()
    {
        if (_bonusUpgrader == null)
            return;

        _bonusUpgrader.UpgradeBonus(_shopItem);
    }

    public void SetupBonusUpgrader(BonusUpgrader bonusUpgrader)
    {
        _bonusUpgrader = bonusUpgrader;
        _bonusUpgrader.FoodCountWasChanged += CheckUpgradedState;

        if (_shopItem == null)
            return;

        UpdateBonusInfo();
    }
}

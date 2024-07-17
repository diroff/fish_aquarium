using UnityEngine;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Transform _tileSpawnPoint;
    [SerializeField] private UIShopItem _shopItemPrefab;
    [SerializeField] private BonusUpgrader _bonusUpgrader;

    [SerializeField] private Shop _shop;

    private bool _isCreated = false;

    private void OnEnable()
    {
        _shop.BonusWasLoaded += SetupShop;
    }

    private void OnDisable()
    {
        _shop.BonusWasLoaded -= SetupShop;
    }

    private void SetupShop()
    {
        if (!_isCreated)
        {
            CreateShopGrid();
        }
    }

    private void CreateShopGrid()
    {
        var datas = _shop.Datas;

        foreach (var item in datas)
        {
            var shopItem = Instantiate(_shopItemPrefab, _tileSpawnPoint);
            shopItem.SetupItem(item);
            shopItem.SetupBonusUpgrader(_bonusUpgrader);
        }

        _isCreated = true;
    }
}
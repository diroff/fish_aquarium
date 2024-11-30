using UnityEngine;
using UnityEngine.Events;

public class BonusUpgrader : MonoBehaviour
{
    [SerializeField] private FoodStorageLoader _foodStorage;
    [SerializeField] private BonusProgression _bonusProgression;

    private BonusDatas _datas;
    private ProgressionBonusData _bonusData;

    public UnityAction<ShopItem> BonusWasUpgraded;
    public UnityAction FoodCountWasChanged;

    private int _currentFoodCount;

    private void OnEnable()
    {
        _foodStorage.FoodCountChanged += OnFoodCountChanged;

        if(_foodStorage.IsInitialized)
            _currentFoodCount = _foodStorage.GetData().FoodCount;
    }

    private void OnDisable()
    {
        _foodStorage.FoodCountChanged -= OnFoodCountChanged;
    }

    private void OnFoodCountChanged(int previousCount, int currentCount)
    {
        _currentFoodCount = currentCount;
        FoodCountWasChanged?.Invoke();
    }

    public bool CanUpgradeBonus(ShopItem item)
    {
        CheckDataState();
        _bonusData = _bonusProgression.GetDataFieldFromID(item.BonusData.BonusInfo.ID);

        var cost = item.BonusData.BonusInfo.TotalBonusCost();
        return _currentFoodCount >= cost;
    }

    private void CheckDataState()
    {
        if (_datas == null)
            _datas = _bonusProgression.GetData();
    }

    public void UpgradeBonus(ShopItem item)
    {
        _bonusData = _bonusProgression.GetDataFieldFromID(item.BonusData.BonusInfo.ID);

        if (!CanUpgradeBonus(item))
            return;

        var cost = item.BonusData.BonusInfo.TotalBonusCost();
        _foodStorage.ReduceMoney((int)cost);

        item.BonusData.BonusInfo.AddLevel(1);
        item.BonusData.BonusLevelWasUpdated?.Invoke(item.BonusData.BonusInfo.Level);
        item.ItemWasUpdated?.Invoke();
        BonusWasUpgraded?.Invoke(item);

        _bonusData.SetLevel(item.BonusData.BonusInfo.Level);
        _bonusProgression.SaveData(_datas);
        Debug.Log($"Bonus {item.BonusData.name} was upgraded to level {item.BonusData.BonusInfo.Level}");
    }
}

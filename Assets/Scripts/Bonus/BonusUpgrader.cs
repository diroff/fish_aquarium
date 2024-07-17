using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class BonusUpgrader : MonoBehaviour
{
    [SerializeField] private FoodStorageLoader _foodStorage;
    [SerializeField] private BonusProgression _bonusProgression;

    private BonusDatas _datas;
    private ProgressionBonusData _bonusData;

    public UnityAction<ShopItem> BonusWasUpgraded;

    public bool CanUpgradeBonus(ShopItem item)
    {
        _datas = _bonusProgression.GetData();
        _bonusData = _bonusProgression.GetDataFieldFromID(item.BonusData.BonusInfo.ID);

        var cost = item.BonusData.BonusInfo.TotalBonusCost();
        return _foodStorage.GetData().FoodCount >= cost;
    }

    public void UpgradeBonus(ShopItem item)
    {
        if (CanUpgradeBonus(item))
        {
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
        else
        {
            Debug.Log("Not enough money to upgrade the bonus!");
        }
    }
}

using UnityEngine;

public class BonusUprader : MonoBehaviour
{
    [SerializeField] private BonusProgression _bonusProgression;

    public void UpgradeBonus(BonusData bonus)
    {
        var data = _bonusProgression.GetData();

        var bonusData = _bonusProgression.GetDataFieldFromID(bonus.BonusInfo.ID);

        bonusData.SetLevel(bonusData.Level + 1);

        _bonusProgression.SaveData(data);
    }
}
using UnityEngine;

public class BonusProgressionLoading : MonoBehaviour
{
    [SerializeField] private string _pathToBonusData = "Data/BonusData";
    [SerializeField] private BonusProgression _bonusProgressionData;

    private BonusData[] _bonusData;

    private void Start()
    {
        SetBonusProgression();
    }

    public BonusData[] GetCurrentBonusData()
    {
        if (_bonusData == null)
            _bonusData = Resources.LoadAll<BonusData>(_pathToBonusData);

        return _bonusData;
    }

    private void SetBonusProgression()
    {
        var allBonusData = GetCurrentBonusData();
        var allBonusDataProgression = _bonusProgressionData.GetData();

        foreach (var bonusData in allBonusData)
        {
            foreach (var bonusDataProgression in allBonusDataProgression.Datas)
            {
                if (bonusData.BonusInfo.ID == bonusDataProgression.ID)
                {
                    bonusData.BonusInfo.SetLevel(bonusDataProgression.Level);
                    Debug.Log($"Bonus with id {bonusData.BonusInfo.ID} now has level {bonusData.BonusInfo.Level}");
                    break;
                }
            }
        }
    }
}
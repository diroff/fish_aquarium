using UnityEngine;

public class BonusProgressionLoading : MonoBehaviour
{
    [SerializeField] private string _pathToBonusData = "Data/BonusData";
    [SerializeField] private BonusProgression _bonusProgressionData;

    private void Start()
    {
        SetBonusProgression();
    }

    private void SetBonusProgression()
    {
        var allBonusData = Resources.LoadAll<BonusData>(_pathToBonusData);
        var allBonusDataProgression = _bonusProgressionData.GetData();

        foreach (var bonusData in allBonusData)
        {
            foreach (var bonusDataProgression in allBonusDataProgression.Datas)
            {
                if(bonusData.BonusInfo.ID == bonusDataProgression.ID)
                {
                    bonusData.BonusInfo.SetLevel(bonusDataProgression.Level);
                    Debug.Log($"Bonus with id {bonusData.BonusInfo.ID} now has level {bonusDataProgression.Level}");
                    break;
                }
            }
        }
    }
}
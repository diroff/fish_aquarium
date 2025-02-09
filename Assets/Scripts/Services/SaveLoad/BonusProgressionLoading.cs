using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BonusProgressionLoading : MonoBehaviour
{
    [SerializeField] private string _pathToBonusData = "Data/BonusData";
    [SerializeField] private BonusProgression _bonusProgressionData;

    private BonusData[] _bonusData;

    public UnityAction DataWasLoaded;

    private void Start()
    {
        SetBonusProgression();
    }

    public BonusData[] GetCurrentBonusData()
    {
        if (_bonusData == null)
            PrepareBonusData();

        return _bonusData;
    }

    private void PrepareBonusData()
    {
        var datas = Resources.LoadAll<BonusData>(_pathToBonusData);

        _bonusData = datas
            .Where(data => data.CanBeUpgraded)
            .OrderBy(data => data.BonusInfo.ID)
            .ToArray();
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
                    break;
                }
            }
        }

        DataWasLoaded?.Invoke();
    }
}
using System;
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
        {
            _bonusData = Resources.LoadAll<BonusData>(_pathToBonusData);

            Array.Sort(_bonusData, (x, y) => x.BonusInfo.ID.CompareTo(y.BonusInfo.ID));
        }

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
                    break;
                }
            }
        }

        DataWasLoaded?.Invoke();
        Debug.Log("Progress was loaded!");
    }
}
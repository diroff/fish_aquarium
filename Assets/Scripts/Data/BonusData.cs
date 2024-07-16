using Newtonsoft.Json;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New BonusData", order = 51)]
public class BonusData : ScriptableObject
{
    [SerializeField] private BonusInfo _bonusInfo;

    [SerializeField] private Sprite _bonusIcon;

    public BonusInfo BonusInfo => _bonusInfo;

    public Sprite BonusIcon => _bonusIcon;
}

[Serializable]
public struct BonusInfo
{
    [JsonProperty("_id")]
    [SerializeField] private int _id;

    [JsonProperty("_bonusName")]
    [SerializeField] private string _bonusName;

    [JsonProperty("_level")]
    [SerializeField] private int _level;

    [JsonProperty("_baseBonusTime")]
    [SerializeField] private float _baseBonusTime;

    [JsonProperty("_baseCost")]
    [SerializeField] private float _baseCost;

    [JsonProperty("_costForLevel")]
    [SerializeField] private float _costForLevel;

    [JsonProperty("_bonusTimeForLevel")]
    [SerializeField] private float _bonusTimeForLevel;

    [JsonProperty("_timeBeforeDestroying")]
    [SerializeField] private float _timeBeforeDestroying;

    public int ID => _id;
    public string BonusName => _bonusName;
    public int Level => _level;
    public float BaseBonusTime => _baseBonusTime;
    public float BaseCostTime => _baseCost;
    public float CostForLevel => _costForLevel;
    public float BonusTimeForLevel => _bonusTimeForLevel;
    public float TimeBeforeDestroying => _timeBeforeDestroying;

    public void SetLevel(int level)
    {
        if (level <= 0)
            level = 1;

        _level = level;
    }

    public float TotalBonusTime()
    {
        return (_baseBonusTime + _bonusTimeForLevel) * _level;
    }

    public float TotalBonusCost()
    {
        return (_baseCost + _costForLevel) * Mathf.Pow(_level, 2);
    }
}
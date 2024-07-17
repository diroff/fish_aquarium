using System;
using Newtonsoft.Json;
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
    [JsonProperty("id")]
    [field: SerializeField] public int ID { get; private set; }
    [JsonProperty("bonusName")]
    [field: SerializeField] public string BonusName { get; private set; }

    [JsonProperty("baseBonusTime")]
    [field: SerializeField] public float BaseBonusTime { get; private set; }
    [JsonProperty("baseCost")]
    [field: SerializeField] public float BaseCost { get; private set; }

    [JsonProperty("costForLevel")]
    [field: SerializeField] public float CostForLevel { get; private set; }
    [JsonProperty("bonusTimeForLevel")]
    [field: SerializeField] public float BonusTimeForLevel { get; private set; }

    [JsonProperty("level")]
    [field: SerializeField] public int Level { get; private set; }

    [JsonProperty("timeBeforeDestroying")]
    [field: SerializeField] public float TimeBeforeDestroying { get; private set; }

    public void SetLevel(int level)
    {
        if (level <= 0)
            level = 1;

        Level = level;
    }

    public void AddLevel(int count)
    {
        Level += count;
    }

    public float TotalBonusTime()
    {
        return (BaseBonusTime + BonusTimeForLevel) * Level;
    }

    public float TotalBonusCost()
    {
        return (BaseCost + CostForLevel) * Mathf.Pow(Level, 2);
    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New BonusData", order = 51)]
public class BonusData : ScriptableObject
{
    [SerializeField] private BonusInfo _bonusInfo;
    [SerializeField] private Sprite _bonusIcon;
    [SerializeField] private bool _canBeUpgraded = true;

    public BonusInfo BonusInfo => _bonusInfo;
    public Sprite BonusIcon => _bonusIcon;
    public bool CanBeUpgraded => _canBeUpgraded;

    public UnityAction BonusWasUpdated;
    public UnityAction<int> BonusLevelWasUpdated;
}

[Serializable]
public class BonusInfo
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string BonusName { get; private set; }

    [field: SerializeField] public float BaseBonusTime { get; private set; }
    [field: SerializeField] public float BaseCost { get; private set; }

    [field: SerializeField] public float CostForLevel { get; private set; }
    [field: SerializeField] public float BonusTimeForLevel { get; private set; }

    [field: SerializeField] public int Level { get; private set; }

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
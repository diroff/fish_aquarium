using UnityEngine;

[CreateAssetMenu(fileName = "New BonusData", order = 51)]
public class BonusData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _bonusName;
    [SerializeField] private Sprite _bonusIcon;
    [SerializeField] private float _timeBeforeDestroying = 5f;

    [SerializeField] private float _baseBonusTime;
    [SerializeField] private float _baseCost;

    [SerializeField] private float _costForLevel;
    [SerializeField] private float _bonusTimeForLevel;

    private int _level = 1;

    public int ID => _id;
    public Sprite BonusIcon => _bonusIcon;
    public string BonusName => _bonusName;
    public float TimeBeforeDestroying => _timeBeforeDestroying;

    public float BaseBonusTime => _baseBonusTime;
    public float BaseCostTime => _baseCost;

    public float CostForLevel => _costForLevel;
    public float BonusTimeForLevel => _bonusTimeForLevel;
    
    public int Level => _level;

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
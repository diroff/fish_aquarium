using UnityEngine;

[CreateAssetMenu(fileName = "New BonusData", order = 51)]
public class BonusData : ScriptableObject
{
    [SerializeField] private Sprite _bonusIcon;
    [SerializeField] private float _timeBeforeDestroying = 5f;

    [SerializeField] private float _baseBonusTime;
    [SerializeField] private float _baseCost;

    [SerializeField] private float _costForLevel;
    [SerializeField] private float _bonusTimeForLevel;

    public float TimeBeforeDestroying => _timeBeforeDestroying;
    public Sprite BonusIcon => _bonusIcon;

    public float BaseBonusTime => _baseBonusTime;
    public float BaseCostTime => _baseCost;

    public float CostForLevel => _costForLevel;
    public float BonusTimeForLevel => _bonusTimeForLevel;

    private int _level = 1;

    public void AddLevel()
    {
        _level++;
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
using UnityEngine;
using UnityEngine.Events;

public class OnetimeBonus : Bonus, IBonusDependencies
{
    [SerializeField] protected ObjectPool Pool;
    [SerializeField] protected LevelEnemies LevelEnemies;

    private int _availableCount;

    public UnityAction<int, int> BonusCountWasChanged;

    public override void UseBonus()
    {
        if (_availableCount <= 0)
        {
            Debug.Log("No available bonuses!");
            return;
        }

        base.UseBonus();

        ReduceAvailableBonus();
        PerfomBonusEffect();
    }

    public void AddAvailableBonus(int count)
    {
        if (count < 0)
            return;

        _availableCount += count;
        BonusCountWasChanged?.Invoke(BonusData.BonusInfo.ID, _availableCount);
    }

    public void ReduceAvailableBonus()
    {
        _availableCount--;
        BonusCountWasChanged?.Invoke(BonusData.BonusInfo.ID, _availableCount);
    }

    protected virtual void PerfomBonusEffect() { }

    public void SetLevelEnemies(LevelEnemies levelEnemies)
    {
        LevelEnemies = levelEnemies;
    }

    public void SetObjectPool(ObjectPool objectPool)
    {
        Pool = objectPool;
    }
}
using UnityEngine;
using UnityEngine.Events;

public class OnetimeBonus : Bonus, IBonusDependencies
{
    [SerializeField] protected ObjectPool Pool;
    [SerializeField] protected LevelEnemies LevelEnemies;

    private int _availableCount;

    public UnityAction<int> BonusCountWasChanged;

    private void Start()
    {
        _availableCount = 3; // For test
        BonusCountWasChanged?.Invoke(_availableCount);
    }

    public override void UseBonus()
    {
        if (_availableCount <= 0)
        {
            Debug.Log("No available bonuses!");
            return;
        }

        base.UseBonus();

        _availableCount--;
        BonusCountWasChanged?.Invoke(_availableCount);
        PerfomBonusEffect();
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
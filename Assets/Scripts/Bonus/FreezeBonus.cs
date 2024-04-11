using UnityEngine;

public class FreezeBonus : Bonus, IBonusDependencies
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private LevelEnemies _levelEnemies;

    public override void UseBonus()
    {
        base.UseBonus();

        foreach (var enemy in _pool.CurrentEnemies)
            enemy.FreezeMoving();

        _levelEnemies.FreezeSpawn();
    }

    public override void StopBonus()
    {
        base.StopBonus();

        foreach (var enemy in _pool.CurrentEnemies)
            enemy.UnfreezeMoving();

        _levelEnemies.UnFreezeSpawn();
    }

    public void SetObjectPool(ObjectPool objectPool)
    {
        _pool = objectPool;
    }

    public void SetLevelEnemies(LevelEnemies levelEnemies)
    {
        _levelEnemies = levelEnemies;
    }
}
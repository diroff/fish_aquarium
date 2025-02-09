using UnityEngine;

public class FreezeBonus : Bonus, IBonusDependencies
{
    [SerializeField] protected ObjectPool Pool;
    [SerializeField] protected LevelEnemies LevelEnemies;

    public override void UseBonus()
    {
        base.UseBonus();

        foreach (var enemy in Pool.CurrentEnemies)
            enemy.FreezeMoving();

        LevelEnemies.FreezeSpawn();
    }

    public override void StopBonus()
    {
        base.StopBonus();

        foreach (var enemy in Pool.CurrentEnemies)
            enemy.UnfreezeMoving();

        LevelEnemies.UnFreezeSpawn();
    }

    public void SetLevelEnemies(LevelEnemies levelEnemies)
    {
        LevelEnemies = levelEnemies;
    }

    public void SetObjectPool(ObjectPool objectPool)
    {
        Pool = objectPool;
    }
}
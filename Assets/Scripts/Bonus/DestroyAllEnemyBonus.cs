using System.Collections.Generic;
using UnityEngine;

public class DestroyAllEnemyBonus : Bonus, IBonusDependencies
{
    [SerializeField] protected ObjectPool Pool;
    [SerializeField] protected LevelEnemies LevelEnemies;

    public override void UseBonus()
    {
        base.UseBonus();

        var enemiesToDestroy = new List<Enemy>(Pool.CurrentEnemies);

        foreach (var enemy in enemiesToDestroy)
            Player.EatOtherCreature(Player, enemy);
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
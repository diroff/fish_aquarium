using System.Collections.Generic;
using UnityEngine;

public class DestroyAllEnemyBonus : Bonus, IBonusDependencies
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private LevelEnemies _levelEnemies;

    public override void UseBonus()
    {
        base.UseBonus();

        var enemiesToDestroy = new List<Enemy>(_pool.CurrentEnemies);

        foreach (var enemy in enemiesToDestroy)
            Player.EatOtherCreature(Player, enemy);
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
public class FreezeBonus : OnetimeBonus
{
    protected override void PerfomBonusEffect()
    {
        base.PerfomBonusEffect();

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
}
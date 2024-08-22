using System.Collections.Generic;

public class DestroyAllEnemyBonus : OnetimeBonus
{
    protected override void PerfomBonusEffect()
    {
        base.PerfomBonusEffect();

        var enemiesToDestroy = new List<Enemy>(Pool.CurrentEnemies);

        foreach (var enemy in enemiesToDestroy)
            Player.EatOtherCreature(Player, enemy);
    }
}
using UnityEngine;

public class LevelBoostBonus : Bonus
{
    [SerializeField] private int _valueBonus;

    private int _levelOnStart;

    public override void UseBonus()
    {
        base.UseBonus();
        _levelOnStart = Player.Level;
        Player.AddLevel(_levelOnStart * (_valueBonus - 1));
    }

    public override void StopBonus()
    {
        Player.AddLevel(-(_levelOnStart * (_valueBonus-1)));
        base.StopBonus();
    }
}
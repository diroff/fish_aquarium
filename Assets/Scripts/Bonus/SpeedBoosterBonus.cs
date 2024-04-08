using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoosterBonus : Bonus
{
    [SerializeField] private int _valueBonus;

    public override void UseBonus()
    {
        base.UseBonus();
        Player.AddMaxSpeed(_valueBonus);
    }

    public override void StopBonus()
    {
        Player.AddMaxSpeed(-_valueBonus);
        base.StopBonus();
    }
}
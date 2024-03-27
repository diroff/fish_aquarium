using UnityEngine;

public class FoodBoostBonus : Bonus
{
    [SerializeField] private int _valueBonus;

    public override void UseBonus()
    {
        base.UseBonus();
        Player.AddFoodMultiplier(_valueBonus);
    }

    public override void StopBonus()
    {
        Player.AddFoodMultiplier(-_valueBonus);
        base.StopBonus();
    }
}
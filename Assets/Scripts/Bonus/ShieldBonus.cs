using UnityEngine;

public class ShieldBonus : Bonus
{
    private GameObject _shield;

    public override void Interact(Creature creature)
    {
        base.Interact(creature);
    }

    public override void UseBonus()
    {
        base.UseBonus();
        _shield = Player.ShieldPlacement;
        _shield.SetActive(true);
        Player.SetShield(this);
    }

    public override void StopBonus()
    {
        base.StopBonus();

        if (_shield == null)
            return;

        _shield.SetActive(false);
        Player.SetShield(null);
    }
}
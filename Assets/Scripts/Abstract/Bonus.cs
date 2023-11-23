using System.Collections;
using UnityEngine;

public abstract class Bonus : MonoBehaviour, IInteractable
{
    [SerializeField] private BonusData _bonusData;

    protected float CurrentTime;
    protected Player Player;

    public BonusData BonusData => _bonusData;

    public virtual void Interact(Creature creature)
    {
        Player = creature as Player;
        UseBonus();
    }

    public virtual void UseBonus()
    {
        CurrentTime = _bonusData.BonusTime;
        StartCoroutine(TimeChecker());
    }

    public abstract void StopBonus();

    protected IEnumerator TimeChecker()
    {
        while (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            yield return null;
        }

        StopBonus();
    }
}
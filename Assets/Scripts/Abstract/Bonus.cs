using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Bonus : MonoBehaviour, IInteractable
{
    [SerializeField] private BonusData _bonusData;
    
    public UnityEvent BonusStarted;
    public UnityEvent BonusEnded;
    public UnityEvent<float> BonusTimeChanged;

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
        BonusStarted?.Invoke();
        StartCoroutine(TimeChecker());
    }

    public virtual void StopBonus()
    {
        BonusEnded?.Invoke();
    }

    protected IEnumerator TimeChecker()
    {
        while (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            BonusTimeChanged?.Invoke(CurrentTime);
            yield return null;
        }

        StopBonus();
    }
}
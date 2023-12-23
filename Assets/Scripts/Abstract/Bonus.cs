using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public abstract class Bonus : MonoBehaviour, IInteractable
{
    [SerializeField] private BonusData _bonusData;
    
    public UnityEvent BonusStarted;
    public UnityEvent BonusEnded;
    public UnityEvent<float> BonusTimeChanged;

    protected float CurrentTime;
    protected Player Player;

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    public BonusData BonusData => _bonusData;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Interact(Creature creature)
    {
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;

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
        Destroy(this);
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
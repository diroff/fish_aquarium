using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public abstract class Bonus : MonoBehaviour, IInteractable
{
    [SerializeField] private BonusData _bonusData;
    
    public UnityEvent BonusStarted;
    public UnityEvent BonusEnded;
    public UnityAction<float, float> BonusTimeChanged;

    protected float CurrentTime;
    protected Player Player;

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    private bool _bonusWasTaked = false;

    private float _currentTimeToDestroy = 0f;

    public BonusData BonusData => _bonusData;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(TimeDestroyChecker());
    }

    public virtual void Interact(Creature creature)
    {
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;

        Player = creature as Player;
        _bonusWasTaked = true;
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
        Destroy(gameObject);
    }

    protected IEnumerator TimeChecker()
    {
        while (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            BonusTimeChanged?.Invoke(CurrentTime, _bonusData.BonusTime);
            yield return null;
        }

        StopBonus();
    }

    private IEnumerator TimeDestroyChecker()
    {
        while (_currentTimeToDestroy <= _bonusData.TimeBeforeDestroying && !_bonusWasTaked)
        {
            _currentTimeToDestroy += Time.deltaTime;
            yield return null;
        }

        if (!_bonusWasTaked)
            Destroy(gameObject);
    }
}
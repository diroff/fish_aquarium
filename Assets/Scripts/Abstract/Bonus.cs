using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public abstract class Bonus : MonoBehaviour, IInteractable
{
    [SerializeField] private BonusData _bonusData;

    [SerializeField] private bool _isDestroyOnEnd = true;

    protected float CurrentTime;
    protected Player Player;

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    private bool _bonusWasTaked = false;
    private float _currentTimeToDestroy = 0f;

    public BonusData BonusData => _bonusData;
    public bool BonusWasTaked => _bonusWasTaked;

    public UnityEvent BonusStarted;
    public UnityEvent BonusEnded;
    public UnityAction<float, float> BonusTimeChanged;

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
        UseBonus();
    }

    public virtual void UseBonus()
    {
        _bonusWasTaked = true;
        CurrentTime = _bonusData.BonusInfo.TotalBonusTime();
        BonusStarted?.Invoke();
        StartCoroutine(TimeChecker());
    }

    public virtual void StopBonus()
    {
        BonusEnded?.Invoke();
        _bonusWasTaked = false;

        if (_isDestroyOnEnd)
            Destroy(gameObject);
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    protected IEnumerator TimeChecker()
    {
        while (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            BonusTimeChanged?.Invoke(CurrentTime, _bonusData.BonusInfo.TotalBonusTime());
            yield return null;
        }

        StopBonus();
    }

    private IEnumerator TimeDestroyChecker()
    {
        while (_currentTimeToDestroy <= _bonusData.BonusInfo.TimeBeforeDestroying && !_bonusWasTaked)
        {
            _currentTimeToDestroy += Time.deltaTime;
            yield return null;
        }

        if (!_bonusWasTaked && _isDestroyOnEnd)
            Destroy(gameObject);
    }
}
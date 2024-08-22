using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : Creature
{
    [SerializeField] private GameObject _shieldPlacement;
    [SerializeField] private GameObject _immortalPlacement;
    [SerializeField] private DynamicJoystick _joystick;

    private int _foodCount;
    private int _foodMultiplier;

    private Bonus _shieldBonus;

    private float _currentImmortalTime = 0f;

    public int FoodCount => _foodCount;
    public GameObject ShieldPlacement => _shieldPlacement;

    public UnityAction<int, int> FoodCountChanged;
    public UnityAction WasRespawn;

    protected override void Start()
    {
        base.Start();
        SetLevel(StartLevel);
        LevelChanged?.Invoke(0, CurrentLevel);
    }

    protected override void Update()
    {
        base.Update();
        GetInput();
    }

    public override void Die()
    {
        if (_shieldBonus != null)
        {
            _shieldBonus.StopBonus();
            SetImmortallity(1f);
            return;
        }

        base.Die();
        gameObject.SetActive(false);
    }

    protected override void EnableImmortality()
    {
        base.EnableImmortality();
        _immortalPlacement.SetActive(true);
    }

    protected override void DisableImmortality()
    {
        base.DisableImmortality();
        _immortalPlacement.SetActive(false);
    }

    public void Respawn()
    {
        WasRespawn?.Invoke();
        gameObject.SetActive(true);
        SetImmortallity(2f);
    }

    private void SetImmortallity(float immortalTime)
    {
        StartCoroutine(StartImmortallity(immortalTime));
    }

    private IEnumerator StartImmortallity(float immortalTime)
    {
        EnableImmortality();
        _currentImmortalTime = 0f;

        while(_currentImmortalTime < immortalTime)
        {
            _currentImmortalTime += Time.deltaTime;
            yield return null;
        }

        DisableImmortality();
    }

    public void AddFood(int count)
    {
        bool noMultiplier = _foodMultiplier == 0;

        if (noMultiplier)
            _foodMultiplier = 1;

        int previousValue = _foodCount;

        _foodCount += (count * _foodMultiplier);
        FoodCountChanged?.Invoke(previousValue, _foodCount);

        if (noMultiplier)
            _foodMultiplier = 0;
    }

    public void SpendFood(int count)
    {
        int previousValue = _foodCount;

        _foodCount -= count;
        FoodCountChanged?.Invoke(previousValue, _foodCount);
    }

    public void AddFoodMultiplier(int value)
    {
        _foodMultiplier += value;
    }

    public void SetShield(ShieldBonus bonus)
    {
        _shieldBonus = bonus;
    }

    private void GetInput()
    {
        InputVector = new Vector2(_joystick.Horizontal, _joystick.Vertical);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        interactable.Interact(this);
    }
}
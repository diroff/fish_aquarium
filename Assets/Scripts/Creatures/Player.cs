using UnityEngine;
using UnityEngine.Events;

public class Player : Creature
{
    [SerializeField] private GameObject _shield;

    private int _foodCount;
    private int _foodMultiplier;

    private Bonus _shieldBonus;

    public int FoodCount => _foodCount;

    public UnityAction<int> FoodCountChanged;

    protected override void Update()
    {
        base.Update();
        GetInput();
    }

    public override void Die()
    {
        if (_shieldBonus != null)
        {
            DisableShield();
            return;
        }

        base.Die();
        Destroy(gameObject);
    }

    public void AddFood(int count)
    {
        bool noMultiplier = _foodMultiplier == 0;

        if (noMultiplier)
            _foodMultiplier = 1;

        _foodCount += (count * _foodMultiplier);
        FoodCountChanged?.Invoke(_foodCount);

        if (noMultiplier)
            _foodMultiplier = 0;
    }

    public void SpendFood(int count)
    {
        _foodCount -= count;
        FoodCountChanged?.Invoke(_foodCount);
    }

    public void AddFoodMultiplier(int value)
    {
        _foodMultiplier += value;
    }

    public void UseBonus(Bonus bonus)
    {
        if (bonus.GetType() == typeof(ShieldBonus))
            UseShield(bonus);
    }

    private void UseShield(Bonus bonus)
    {
        _shieldBonus = bonus;

        bonus.BonusStarted.AddListener(EnableShield);
        bonus.BonusEnded.AddListener(DisableShield);
    }

    private void EnableShield()
    {
        _shield.SetActive(true);
    }

    private void DisableShield()
    {
        _shieldBonus.BonusStarted.RemoveListener(EnableShield);
        _shieldBonus.BonusEnded.RemoveListener(DisableShield);

        _shield.SetActive(false);
        _shieldBonus = null;
    }

    private void GetInput()
    {
        InputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        interactable.Interact(this);
    }
}
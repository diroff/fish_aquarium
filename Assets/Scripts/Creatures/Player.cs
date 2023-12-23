using UnityEngine;
using UnityEngine.Events;

public class Player : Creature
{
    [SerializeField] private GameObject _shieldPlacement;

    private int _foodCount;
    private int _foodMultiplier;

    private Bonus _shieldBonus;

    public int FoodCount => _foodCount;
    public GameObject ShieldPlacement => _shieldPlacement;

    public UnityAction<int, int> FoodCountChanged;

    protected override void Start()
    {
        base.Start();
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
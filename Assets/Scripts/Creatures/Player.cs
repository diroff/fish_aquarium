using UnityEngine;

public class Player : Creature
{
    [SerializeField] private GameObject _shield;

    private Bonus _shieldBonus;

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
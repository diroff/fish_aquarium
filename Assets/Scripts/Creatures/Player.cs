using UnityEngine;

public class Player : Creature
{
    protected override void Update()
    {
        base.Update();
        GetInput();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public void UseBonus(Bonus bonus)
    {

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
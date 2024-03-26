using UnityEngine;

public class Enemy : Creature, IInteractable
{
    private float _movementSpeed;

    protected override void Start()
    {
        UpdateScale();
        SetRandomSpeed();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void SetRandomSpeed()
    {
        _movementSpeed = Random.Range(0.1f, 0.49f);
    }

    public void Move(bool isRight)
    {
        SetRandomSpeed();
        _movementSpeed *= isRight ? 1 : -1;
        InputVector = new Vector2(_movementSpeed, 0);
    }

    public void Interact(Creature creature)
    {
        TryToEatOtherCreature(creature);
    }

    public void SetEnemyLevel(int level)
    {
        if (level <= 0)
            throw new System.Exception("Creature level can't be less 0!");

        SetLevel(level);
    }
}
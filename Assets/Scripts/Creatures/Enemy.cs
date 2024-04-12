using UnityEngine;
using UnityEngine.Events;

public class Enemy : Creature, IInteractable
{
    [Header("Enemy settings")]
    [SerializeField] private int _creatureID;

    private float _movementSpeed;

    protected float _xMovementValue;

    public int CreatureID => _creatureID;

    public UnityAction WasDestroyed;

    protected override void Start()
    {
        base.Start();
        PrepareEnemy();
    }

    protected override void Update()
    {
        base.Update();
    }

    public virtual void PrepareEnemy()
    {
        UpdateScale();
        SetRandomSpeed();
    }

    private void SetRandomSpeed()
    {
        _movementSpeed = Random.Range(0.1f, 0.49f);
    }

    public virtual void Move(bool isRight)
    {
        _movementSpeed *= isRight ? 1 : -1;
        InputVector = new Vector2(_movementSpeed, InputVector.y);
        _xMovementValue = InputVector.x;
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
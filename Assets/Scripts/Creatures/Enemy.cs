using System.Collections;
using UnityEngine;

public class Enemy : Creature, IInteractable
{
    private float _movementSpeed;
    private float _patrolingTime;

    protected override void Start()
    {
        base.Start();
        SetRandomSpeed();
        SetPatrolingTime();
        StartCoroutine(Patroling());
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void SetRandomSpeed()
    {
        _movementSpeed = Random.Range(0.1f, 0.49f);
    }

    private void SetPatrolingTime()
    {
        _patrolingTime = Random.Range(0.3f, 1f);
    }

    private IEnumerator Patroling()
    {
        InputVector = new Vector2(_movementSpeed, 0);

        yield return new WaitForSeconds(_patrolingTime);

        InputVector = new Vector2(-_movementSpeed, 0);

        yield return new WaitForSeconds(_patrolingTime);
        StartCoroutine(Patroling());
    }

    public void Interact(Creature creature)
    {
        TryToEatOtherCreature(creature);
    }
}
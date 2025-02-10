using System.Collections;
using UnityEngine;

public class TurningEnemy : Enemy
{
    [SerializeField] private float _minChangeTime = 3f;
    [SerializeField] private float _maxChangeTime = 7f;

    private Coroutine _changeDirectionCoroutine;

    public override void PrepareEnemy()
    {
        base.PrepareEnemy();

        if (_changeDirectionCoroutine != null)
            StopCoroutine(_changeDirectionCoroutine);

        _changeDirectionCoroutine = StartCoroutine(ChangeDirectionRandomly());
    }

    private IEnumerator ChangeDirectionRandomly()
    {
        float changeTime = Random.Range(_minChangeTime, _maxChangeTime);

        yield return new WaitForSeconds(changeTime);

        _xMovementValue *= -1;
        InputVector = new Vector2(_xMovementValue, InputVector.y);
    }
}
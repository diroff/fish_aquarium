using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : Enemy
{
    private float _verticalMovementTime = 1f;
    private float _waveMagnitude = 1f;

    private float _currentTime = 0;

    private void OnDisable()
    {
        StopCoroutine(WaveMoving());
    }

    public override void PrepareEnemy()
    {
        base.PrepareEnemy();
        CalculateWaveMovement();
        StartCoroutine(WaveMoving());
    }

    private void CalculateWaveMovement()
    {
        _verticalMovementTime = Random.Range(1f, 2f);
        _waveMagnitude = Random.Range(0.2f, 0.8f);
    }

    private IEnumerator WaveMoving()
    {
        while (true)
        {
            _currentTime += Time.deltaTime;
            float verticalMovement = Mathf.Sin(_currentTime * 2 * Mathf.PI / _verticalMovementTime) * _waveMagnitude;

            InputVector = new Vector2(_xMovementValue, verticalMovement);

            yield return null;
        }
    }
}
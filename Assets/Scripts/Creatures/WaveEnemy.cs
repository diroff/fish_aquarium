using System.Collections;
using UnityEngine;

public class WaveEnemy : Enemy
{
    [SerializeField] private float _minVerticalMovementTime;
    [SerializeField] private float _maxVerticalMovementTime;

    [SerializeField] private float _minWaveMagnitude;
    [SerializeField] private float _maxWaveMagnitude;

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
        _verticalMovementTime = Random.Range(_minVerticalMovementTime, _maxVerticalMovementTime);
        _waveMagnitude = Random.Range(_minWaveMagnitude, _maxWaveMagnitude);
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
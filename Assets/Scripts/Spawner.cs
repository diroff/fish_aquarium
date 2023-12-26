using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private bool _isRightSpawner;

    [Header("Spawn Range")]
    [SerializeField] private float _xMinPoint;
    [SerializeField] private float _xMaxPoint;
    [SerializeField] private float _yMinPoint;
    [SerializeField] private float _yMaxPoint;

    private Vector3 _spawnPoint;
    private Enemy _enemyPrefab;

    private void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        Spawn();

        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnTimer());
    }

    private void CalculateSpawnPosition()
    {
        var xPosition = Random.Range(_xMinPoint, _xMaxPoint);
        var yPosition = Random.Range(_yMinPoint, _yMaxPoint);

        _spawnPoint = new Vector3(xPosition, yPosition, 0f);
    }

    public void Spawn()
    {
        CalculateSpawnPosition();

        _enemyPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];

        var enemy = Instantiate(_enemyPrefab, _spawnPoint, Quaternion.identity);
        enemy.Move(!_isRightSpawner);
    }
}
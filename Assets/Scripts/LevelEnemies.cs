using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemies : MonoBehaviour
{
    [SerializeField] private List<EnemySetting> _availableEnemies;

    [Header("Settings")]
    [SerializeField] private float _spawnDelay;

    [SerializeField] private float _syncSpawnMinDelay = 0.01f; 
    [SerializeField] private float _syncSpawnMaxDelay = 0.02f; 

    [Space]
    [SerializeField] private LevelSpawners _levelSpawners;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator SpawnTimer()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _levelSpawners.Spawners.Count; i++)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_syncSpawnMinDelay, _syncSpawnMaxDelay));
            SpawnEnemy();
        }

        yield return new WaitForSeconds(_spawnDelay);
        StartCoroutine(Spawn());
    }

    private void SpawnEnemy()
    {
        var enemy = _availableEnemies[UnityEngine.Random.Range(0, _availableEnemies.Count)];
        int level = UnityEngine.Random.Range(enemy.MinLevel, enemy.MaxLevel);

        _levelSpawners.Spawn(enemy.EnemyPrefab, level);
    }
}

[Serializable]
public class EnemySetting
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _minLevel = 1; 
    [SerializeField] private int _maxLevel = 1; 
    [SerializeField] private int _maxCount; 

    public Enemy EnemyPrefab => _enemyPrefab;

    public int MinLevel => _minLevel;
    public int MaxLevel => _maxLevel;
    public int MaxCount => _maxCount;
}

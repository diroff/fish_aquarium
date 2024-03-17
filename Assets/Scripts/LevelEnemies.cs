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

    private List<EnemySetting> _currentEnemies = new List<EnemySetting>();

    private int _currentEnemyIndex;

    private void Start()
    {
        PrepareEnemies();
        StartCoroutine(Spawn());
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
        TakeEnemy();

        var enemy = _availableEnemies[UnityEngine.Random.Range(0, _availableEnemies.Count)];
        enemy.SetLevel(UnityEngine.Random.Range(enemy.MinLevel, enemy.MaxLevel));

        _levelSpawners.Spawn(enemy.EnemyPrefab, enemy.CurrentLevel);

        _currentEnemies.RemoveAt(_currentEnemyIndex);
    }

    private void TakeEnemy()
    {
        if (_currentEnemies.Count == 0)
            PrepareEnemies();

        _currentEnemyIndex = UnityEngine.Random.Range(0, _currentEnemies.Count);
    }

    private void PrepareEnemies()
    {
        foreach (var enemy in _availableEnemies)
        {
            for (int i = 0; i < enemy.MaxCount; i++)
            {
                _currentEnemies.Add(enemy);
            }
        }
    }
}

[Serializable]
public class EnemySetting
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _minLevel = 1; 
    [SerializeField] private int _maxLevel = 1; 
    [SerializeField] private int _maxCount;

    private int _currentLevel = 1;

    public Enemy EnemyPrefab => _enemyPrefab;

    public int MinLevel => _minLevel;
    public int MaxLevel => _maxLevel;
    public int MaxCount => _maxCount;
    public int CurrentLevel => _currentLevel;   

    public void SetLevel(int level)
    {
        _currentLevel = level;
    }
}

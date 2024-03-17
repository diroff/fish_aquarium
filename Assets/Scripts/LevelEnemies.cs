using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemies : MonoBehaviour
{
    [SerializeField] private List<EnemySetting> _availableEnemies;

    [Space]
    [SerializeField] private LevelSpawners _levelSpawners;

    private void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        var enemy = _availableEnemies[UnityEngine.Random.Range(0, _availableEnemies.Count)];

        _levelSpawners.Spawn(enemy.EnemyPrefab, enemy.MinLevel);

        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnTimer());
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

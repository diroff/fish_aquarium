using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _poolCountModificator;

    private Dictionary<Enemy, Queue<Enemy>> _poolDictionary = new Dictionary<Enemy, Queue<Enemy>>();
    private Dictionary<Enemy, Enemy> _instanceToPrefabMap = new Dictionary<Enemy, Enemy>();

    private void OnEnable()
    {
        if (_poolDictionary.Count == 0)
            return;

        foreach (var dictionary in _poolDictionary)
        {
            dictionary.Key.Died += ReturnToPool;
        }
    }

    private void OnDisable()
    {
        if (_poolDictionary.Count == 0)
            return;

        foreach (var dictionary in _poolDictionary)
        {
            dictionary.Key.Died -= ReturnToPool;
        }
    }

    public void Initialize(List<EnemySetting> availableEnemies)
    {
        foreach (var enemySetting in availableEnemies)
        {
            CreatePool(enemySetting.EnemyPrefab, enemySetting.MaxCount * _poolCountModificator);
        }
    }

    private void CreatePool(Enemy prefab, int initialCount)
    {
        if (!_poolDictionary.ContainsKey(prefab))
        {
            var newQueue = new Queue<Enemy>();
            _poolDictionary[prefab] = newQueue;

            for (int i = 0; i < initialCount; i++)
            {
                Enemy newEnemy = Instantiate(prefab);
                newEnemy.gameObject.SetActive(false);
                newQueue.Enqueue(newEnemy);
                _instanceToPrefabMap[newEnemy] = prefab;
                newEnemy.Died += ReturnToPool;
            }
        }
    }

    public Enemy SpawnFromPool(Enemy prefab, Vector3 position, Quaternion rotation, int level, bool isRightSpawner)
    {
        if (!_poolDictionary.ContainsKey(prefab) || _poolDictionary[prefab].Count == 0)
            CreatePool(prefab, 1);

        Enemy enemyToSpawn;

        if (_poolDictionary[prefab].Count == 0)
        {
            enemyToSpawn = Instantiate(prefab);
            Debug.Log("Недостаточно объектов! Создаем еще");
            _instanceToPrefabMap[enemyToSpawn] = prefab;
        }
        else
            enemyToSpawn = _poolDictionary[prefab].Dequeue();

        enemyToSpawn.gameObject.SetActive(true);
        enemyToSpawn.transform.position = position;
        enemyToSpawn.transform.rotation = rotation;
        enemyToSpawn.SetEnemyLevel(level);
        enemyToSpawn.Move(isRightSpawner);

        return enemyToSpawn;
    }

    public void ReturnToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);

        if (_instanceToPrefabMap.TryGetValue(enemy, out Enemy prefab) && _poolDictionary.ContainsKey(prefab))
        {
            _poolDictionary[prefab].Enqueue(enemy);
        }
        else
        {
            Debug.LogWarning("Trying to return an object to a pool that doesn't exist.");
        }
    }

    private void ReturnToPool(Creature creature)
    {
        ReturnToPool(creature as Enemy);
    }
}
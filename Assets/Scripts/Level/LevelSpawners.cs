using System.Collections.Generic;
using UnityEngine;

public class LevelSpawners : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private ObjectPool _objectPool;

    private int _lastSpawnerNumber = 0;

    public List<Spawner> Spawners => _spawners;

    public void Spawn(Enemy enemy, int level)
    {
        if (_lastSpawnerNumber >= _spawners.Count)
            _lastSpawnerNumber = 0;

        _spawners[_lastSpawnerNumber].Spawn(enemy, level, _objectPool);

        _lastSpawnerNumber++;
    }
}

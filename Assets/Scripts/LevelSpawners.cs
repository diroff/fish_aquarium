using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSpawners : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;

    private int _lastSpawnerNumber = 0;

    public List<Spawner> Spawners => _spawners;

    public void Spawn(Enemy enemy, int level)
    {
        if (_lastSpawnerNumber >= _spawners.Count)
            _lastSpawnerNumber = 0;

        _spawners[_lastSpawnerNumber].Spawn(enemy, level);

        _lastSpawnerNumber++;
    }
}

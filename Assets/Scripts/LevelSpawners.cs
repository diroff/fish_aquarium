using System.Collections.Generic;
using UnityEngine;

public class LevelSpawners : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;

    private int _lastSpawnerNumber = 0;

    public void Spawn(Enemy enemy, int level)
    {
        if (_lastSpawnerNumber >= _spawners.Count)
            _lastSpawnerNumber = 0;

        _spawners[_lastSpawnerNumber].Spawn(enemy, level);

        Debug.Log($"Enemy was spawned on {_lastSpawnerNumber} spawner with {level} level");

        _lastSpawnerNumber++;
    }
}

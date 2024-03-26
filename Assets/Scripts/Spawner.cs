using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool _isRightSpawner;

    [Header("Spawn Range")]
    [SerializeField] private float _xMinPoint;
    [SerializeField] private float _xMaxPoint;
    [SerializeField] private float _yMinPoint;
    [SerializeField] private float _yMaxPoint;

    private Vector3 _spawnPoint;

    private void CalculateSpawnPosition()
    {
        Vector3 spawnerPosition = transform.position;

        var xPosition = Random.Range(spawnerPosition.x + _xMinPoint, spawnerPosition.x + _xMaxPoint);
        var yPosition = Random.Range(spawnerPosition.y + _yMinPoint, spawnerPosition.y + _yMaxPoint);

        _spawnPoint = new Vector3(xPosition, yPosition, 0f);
    }

    public void Spawn(Enemy enemyPrefab, int level, ObjectPool objectPool)
    {
        CalculateSpawnPosition();

        objectPool.SpawnFromPool(enemyPrefab, _spawnPoint, Quaternion.identity, level, !_isRightSpawner);
    }
}
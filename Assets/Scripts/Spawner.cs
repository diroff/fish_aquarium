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
        var xPosition = Random.Range(_xMinPoint, _xMaxPoint);
        var yPosition = Random.Range(_yMinPoint, _yMaxPoint);

        _spawnPoint = new Vector3(xPosition, yPosition, 0f);
    }

    public void Spawn(Enemy enemyPrefab, int level)
    {
        CalculateSpawnPosition();

        var enemy = Instantiate(enemyPrefab, _spawnPoint, Quaternion.identity);
        enemy.SetLevel(level);
        enemy.Move(!_isRightSpawner);
    }
}
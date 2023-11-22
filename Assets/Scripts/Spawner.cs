using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private GameObject _spawnPoint;

    public void Spawn()
    {
        Instantiate(_objectToSpawn, _spawnPoint.transform.position, Quaternion.identity);
    }
}
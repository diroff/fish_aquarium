using UnityEngine;

public class SpriteOrderLayerSorting : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;

    private int _sortingIndex = 0;

    private void OnEnable()
    {
        _objectPool.EnemyWasAdded += OnEnemyWasAdded;
        _objectPool.EnemyWasRemoved += OnEnemyWasRemoved;
    }

    private void OnDisable()
    {
        _objectPool.EnemyWasAdded -= OnEnemyWasAdded;
        _objectPool.EnemyWasRemoved -= OnEnemyWasRemoved;
    }

    private void OnEnemyWasAdded(Enemy enemy)
    {
        SortEnemies(enemy);
    }

    private void OnEnemyWasRemoved(Enemy enemy)
    {
        SortEnemies(enemy);
    }

    private void SortEnemies(Enemy enemy)
    {
        var spriteRenderer = enemy.SpritePlacement.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = _sortingIndex;
        _sortingIndex++;
    }
}
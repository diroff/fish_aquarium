using System.Collections.Generic;
using UnityEngine;

public class EffectCreator : MonoBehaviour
{
    [SerializeField] private CreatureDieEffect _effectPrefab;
    [SerializeField] private int _effectCount;

    [SerializeField] private ObjectPool _objectPool;

    private Queue<CreatureDieEffect> _effects = new Queue<CreatureDieEffect>();

    private void Awake()
    {
        for (int i = 0; i < _effectCount; i++)
        {
            var effect = Instantiate(_effectPrefab);
            _effects.Enqueue(effect);
            effect.HideEffect();
        }
    }

    private void OnEnable()
    {
        _objectPool.EnemyWasAdded += OnEnemyWasAdded;
        _objectPool.EnemyWasRemoved += OnEnemyWasRemoved;

        if (_objectPool.CurrentEnemies.Count == 0)
            return;

        foreach (var enemy in _objectPool.CurrentEnemies)
        {
            enemy.DiedOnPosition += TakeEffect;
        }
    }

    private void OnDisable()
    {
        _objectPool.EnemyWasAdded -= OnEnemyWasAdded;
        _objectPool.EnemyWasRemoved -= OnEnemyWasRemoved;

        if (_objectPool.CurrentEnemies.Count == 0)
            return;

        foreach (var enemy in _objectPool.CurrentEnemies)
        {
            enemy.DiedOnPosition -= TakeEffect;
        }
    }

    private void OnEnemyWasAdded(Enemy enemy)
    {
        enemy.DiedOnPosition += TakeEffect;
    }

    private void OnEnemyWasRemoved(Enemy enemy)
    {
        enemy.DiedOnPosition -= TakeEffect;
    }

    public void TakeEffect(Vector2 position)
    {
        var effect = _effects.Dequeue();
        effect.gameObject.SetActive(true);
        effect.ShowEffect(position);
        effect.SetEffectCreator(this);
    }

    public void ReturnToPool(CreatureDieEffect effect)
    {
        effect.gameObject.SetActive(false);
        _effects.Enqueue(effect);
    }
}
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
            CreateEffect();
        }
    }

    private void OnEnable()
    {
        _objectPool.EnemyWasAdded += OnEnemyWasAdded;
        _objectPool.EnemyWasRemoved += OnEnemyWasRemoved;

        if (_objectPool.CurrentEnemies.Count == 0)
            return;

        foreach (var enemy in _objectPool.CurrentEnemies)
            enemy.WasAtedOnPosition += TakeEffect;
    }

    private void OnDisable()
    {
        _objectPool.EnemyWasAdded -= OnEnemyWasAdded;
        _objectPool.EnemyWasRemoved -= OnEnemyWasRemoved;

        if (_objectPool.CurrentEnemies.Count == 0)
            return;

        foreach (var enemy in _objectPool.CurrentEnemies)
            enemy.WasAtedOnPosition -= TakeEffect;
    }

    private void OnEnemyWasAdded(Enemy enemy)
    {
        enemy.WasAtedOnPosition += TakeEffect;
    }

    private void OnEnemyWasRemoved(Enemy enemy)
    {
        enemy.WasAtedOnPosition -= TakeEffect;
    }

    public void TakeEffect(Vector2 position)
    {
        if (_effects.Count == 0)
            CreateEffect();

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

    private void CreateEffect()
    {
        var effect = Instantiate(_effectPrefab);
        _effects.Enqueue(effect);
        effect.HideEffect();
    }
}
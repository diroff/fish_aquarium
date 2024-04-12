using UnityEngine;

public class EatSpecificFishQuest : QuantitativeQuest
{
    [SerializeField] private Enemy _goal;
    [SerializeField] private ObjectPool _pool;

    private void OnEnable()
    {
        _pool.EnemyWasAdded += SubscribeForEnemy;
    }

    private void OnDisable()
    {
        _pool.EnemyWasAdded -= UnSubscribeFromEnemy;
    }

    private void SubscribeForEnemy(Enemy enemy)
    {
        if (enemy.CreatureID != _goal.CreatureID)
            return;

        enemy.WasAtedOnPosition += AddFish;
    }

    private void UnSubscribeFromEnemy(Enemy enemy)
    {
        if (enemy.CreatureID != _goal.CreatureID)
            return;

        enemy.WasAtedOnPosition -= AddFish;
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
    }

    private void AddFish(Vector2 vector2)
    {
        AddCount(1);
    }
}
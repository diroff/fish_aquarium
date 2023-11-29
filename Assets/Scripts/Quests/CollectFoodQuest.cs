using UnityEngine;

public class CollectFoodQuest : QuantitativeQuest
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.FoodCountChanged += AddLevel;
    }

    private void OnDisable()
    {
        _player.FoodCountChanged -= AddLevel;
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
        _player.FoodCountChanged -= AddLevel;
    }

    private void AddLevel(int previousLevel, int currentLevel)
    {
        AddCount(currentLevel - previousLevel);
    }
}
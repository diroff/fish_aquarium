using UnityEngine;

public class AchiveLevelQuest : QuantitativeQuest
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.LevelChanged += AddLevel;
    }

    private void OnDisable()
    {
        _player.LevelChanged -= AddLevel;
    }

    public override void CompleteQuest()
    {
        base.CompleteQuest();
        _player.LevelChanged -= AddLevel;
    }

    private void AddLevel(int previousLevel, int currentLevel)
    {
        AddCount(currentLevel - previousLevel);
    }
}
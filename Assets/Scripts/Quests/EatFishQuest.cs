using UnityEngine;

public class EatFishQuest : QuantitativeQuest
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.FishAted += AddFish;
    }

    private void OnDisable()
    {
        _player.FishAted -= AddFish;
    }

    private void AddFish()
    {
        AddCount(1);
    }
}
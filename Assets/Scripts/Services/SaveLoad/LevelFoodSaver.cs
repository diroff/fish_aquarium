using UnityEngine;

public class LevelFoodSaver : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private Player _player;
    [SerializeField] private FoodStorageLoader _foodStorageLoader;

    private void OnEnable()
    {
        _level.LevelCompleted += SaveFoodCount;
    }

    private void OnDisable()
    {
        _level.LevelCompleted -= SaveFoodCount;
    }

    private void SaveFoodCount()
    {
        _foodStorageLoader.AddMoney(_player.FoodCount);
    }
}
using UnityEngine;
using UnityEngine.Events;

public class UIFoodMultiplyer : MonoBehaviour
{
    [SerializeField] private LevelFoodSaver _foodSaver;

    private void OnEnable()
    {
        if(_foodSaver.Player.FoodCount == 0)
            Destroy(gameObject);
    }

    public void MultiplyFood()
    {
        _foodSaver.SaveFoodCount();
        Destroy(gameObject);
    }
}
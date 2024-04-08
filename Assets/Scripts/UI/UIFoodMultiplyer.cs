using UnityEngine;
using UnityEngine.Events;

public class UIFoodMultiplyer : MonoBehaviour
{
    [SerializeField] private LevelFoodSaver _foodSaver;

    public void MultiplyFood()
    {
        _foodSaver.SaveFoodCount();
        Destroy(gameObject);
    }
}
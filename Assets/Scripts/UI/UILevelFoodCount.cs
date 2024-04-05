using TMPro;
using UnityEngine;

public class UILevelFoodCount : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _foodPanel;
    [SerializeField] private TextMeshProUGUI _foodCount;

    private void OnEnable()
    {
        _player.FoodCountChanged += UpdateFoodCount;
    }

    private void OnDisable()
    {
        _player.FoodCountChanged -= UpdateFoodCount;
    }

    private void UpdateFoodCount(int previousCount, int currentCount)
    {
        _foodPanel.SetActive(true);
        _foodCount.text = currentCount.ToString();
    }
}
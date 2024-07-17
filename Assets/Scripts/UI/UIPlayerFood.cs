using TMPro;
using UnityEngine;

public class UIPlayerFood : MonoBehaviour
{
    [SerializeField] private FoodStorageLoader _foodStorage;
    [SerializeField] private TextMeshProUGUI _foodCountText;

    private void Awake()
    {
        _foodStorage.Load();
    }

    private void OnEnable()
    {
        _foodStorage.FoodCountChanged += OnFoodCountChanged;
        _foodStorage.Load();
    }

    private void OnDisable()
    {
        _foodStorage.FoodCountChanged -= OnFoodCountChanged;
        _foodStorage.Load();
    }

    private void OnFoodCountChanged(int previousValue, int currentValue)
    {
        _foodCountText.gameObject.SetActive(true);
        _foodCountText.text = currentValue.ToString();
    }
}

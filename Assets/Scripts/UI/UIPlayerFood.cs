using TMPro;
using UnityEngine;

public class UIPlayerFood : MonoBehaviour
{
    [SerializeField] private FoodStorageLoader _foodStorage;
    [SerializeField] private TextMeshProUGUI _foodCountText;

    private bool _dataLoaded;

    private void OnEnable()
    {
        _foodStorage.OnStorageServiceCreated += OnStorageServiceCreated;

        if(_dataLoaded)
            _foodStorage.FoodCountChanged += OnFoodCountChanged;
    }

    private void OnDisable()
    {
        _foodStorage.OnStorageServiceCreated -= OnStorageServiceCreated;
        _foodStorage.FoodCountChanged -= OnFoodCountChanged;
    }

    private void OnFoodCountChanged(int previousValue, int currentValue)
    {
        _foodCountText.gameObject.SetActive(true);
        _foodCountText.text = currentValue.ToString();
    }

    private void OnStorageServiceCreated()
    {
        _foodStorage.FoodCountChanged += OnFoodCountChanged;
        _foodStorage.Load();
        _dataLoaded = true;
    }
}

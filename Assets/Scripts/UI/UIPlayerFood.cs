using TMPro;
using UnityEngine;

public class UIPlayerFood : MonoBehaviour
{
    [SerializeField] private FoodStorageLoader _foodStorage;
    [SerializeField] private TextMeshProUGUI _foodCountText;

    private void OnEnable()
    {
        _foodStorage.FoodCountChanged += OnFoodCountChanged;

        if (_foodStorage.IsInitialized)
            Initialize();
    }

    private void OnDisable()
    {
        _foodStorage.FoodCountChanged -= OnFoodCountChanged;
    }

    private void Initialize()
    {
        var data = _foodStorage.GetData();

        if (data == null)
            return;

        OnFoodCountChanged(0, data.FoodCount);
    }

    private void OnFoodCountChanged(int previousValue, int currentValue)
    {
        _foodCountText.gameObject.SetActive(true);
        _foodCountText.text = currentValue.ToString();
    }
}

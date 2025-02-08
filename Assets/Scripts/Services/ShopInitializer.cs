using UnityEngine;

public class ShopInitializer : MonoBehaviour
{
    [SerializeField] private BonusProgressionLoading _dataLoader;
    [SerializeField] private Shop _shop;

    private void Start()
    {
        OnDataLoaded();
    }

    private void OnDataLoaded()
    {
        _shop.Initialize(_dataLoader.GetCurrentBonusData());
        _dataLoader.DataWasLoaded -= OnDataLoaded;
    }
}
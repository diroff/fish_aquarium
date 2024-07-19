using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private BonusProgressionLoading _dataLoader;

    private BonusData[] _data;

    public List<ShopItem> Datas { get; private set; } = new List<ShopItem>();
    public bool IsDataLoaded { get; private set; } = false;

    public UnityAction BonusWasLoaded;

    private void OnEnable()
    {
        if (_data == null)
            OnDataLoaded();

        _dataLoader.DataWasLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _dataLoader.DataWasLoaded -= OnDataLoaded;
    }

    private void OnDataLoaded()
    {
        _data = _dataLoader.GetCurrentBonusData();

        SetData();
    }

    private void SetData()
    {
        foreach (var item in _data)
            Datas.Add(new ShopItem(item));

        IsDataLoaded = true;
        BonusWasLoaded?.Invoke();
    }
}
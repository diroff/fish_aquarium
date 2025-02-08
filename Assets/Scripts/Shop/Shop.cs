using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    private BonusData[] _data;

    public List<ShopItem> Datas { get; private set; } = new List<ShopItem>();
    public bool IsDataLoaded { get; private set; } = false;

    public UnityAction BonusWasLoaded;

    public void Initialize(BonusData[] datas)
    {
        _data = datas;

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
using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusProgression : MonoBehaviour
{
    public const string Key = "BonusData";

    private IStorageService _storageService;
    private BonusDatas _data;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<BonusDatas>(Key, data =>
        {
            if (data == default)
                FirstSave();

            Load();
        });
    }

    public void Load()
    {
        _storageService.Load<BonusDatas>(Key, data =>
        {
            _data = data;
        });
    }

    private void FirstSave()
    {
        BonusDatas data = new BonusDatas();

        var allBonusData = Resources.LoadAll<BonusData>("Data/BonusData");
        
        foreach (var item in allBonusData)
            data.Datas.Add(item.BonusInfo);

        _storageService.Save(Key, data);
    }

    public BonusDatas GetData()
    {
        Load();
        return _data;
    }
}

public class BonusDatas
{
    public List<BonusInfo> Datas = new List<BonusInfo>();
}
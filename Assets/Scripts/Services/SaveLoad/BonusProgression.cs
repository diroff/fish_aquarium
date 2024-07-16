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
        LoadData();
    }

    private void LoadData()
    {
        _storageService.Load<BonusDatas>(Key, data =>
        {
            if (data == null)
                FirstSave();
            else
                _data = data;
        });
    }

    private void FirstSave()
    {
        BonusDatas data = new BonusDatas();

        var allBonusData = Resources.LoadAll<BonusData>("Data/BonusData");

        foreach (var item in allBonusData)
        {
            data.Datas.Add(item.BonusInfo);
        }

        SaveData(data);
    }

    public void SaveData(BonusDatas data)
    {
        _storageService.Save(Key, data, success =>
        {
            if (success)
                Debug.Log("Data saved successfully!");
            else
                Debug.LogError("Failed to save data.");
        });
    }

    public BonusInfo GetDataByID(int id)
    {
        BonusInfo info = new BonusInfo();

        var data = GetData();

        foreach (var item in data.Datas)
        {
            if (item.ID == id)
            {
                info = item;
                break;
            }
        }

        return info;
    }

    public void SaveChangesOnBonusInfo(int id)
    {
        var field = GetDataByID(id);

        var data = GetData();

        foreach (var item in data.Datas)
        {
            if (item.ID == field.ID)
            {
                item.SetLevel(field.Level);
                break;
            }
        }

        SaveData(data);

    }

    [ContextMenu("What loaded?")]
    public void ShowLoadedDatas()
    {
        LoadData();
    }

    public BonusDatas GetData()
    {
        LoadData();
        return _data;
    }
}

[Serializable]
public class BonusDatas
{
    public List<BonusInfo> Datas = new List<BonusInfo>();
}

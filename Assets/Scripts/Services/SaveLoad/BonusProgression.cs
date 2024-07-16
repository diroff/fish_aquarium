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
            {
                FirstSave();
            }
            else
            {
                _data = data;
                Debug.Log("Data loaded:");
                foreach (var item in _data.Datas)
                {
                    Debug.Log($"{item.ID} id; {item.BonusName} name; {item.Level} level");
                }
            }
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
            {
                Debug.Log("Data saved successfully!");
                ShowSaveDatas(data);
            }
            else
            {
                Debug.LogError("Failed to save data.");
            }
        });
    }

    public void ShowSaveDatas(BonusDatas data)
    {
        foreach (var item in data.Datas)
        {
            Debug.Log($"{item.ID} id; {item.BonusName} name; {item.Level} level");
        }
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

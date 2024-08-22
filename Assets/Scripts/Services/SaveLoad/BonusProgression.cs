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
        if (_data != null)
            return;

        bool needToSave = false;

        _storageService.Load<BonusDatas>(Key, data =>
        {
            if (data == null)
                FirstSave();

            else
            {
                _data = data;
                needToSave = NewBonusesWasAdded();
            }
        });

        if(needToSave)
            SaveData(_data);
    }

    private IEnumerable<ProgressionBonusData> GetUpgradableBonuses()
    {
        var allBonusData = Resources.LoadAll<BonusData>("Data/BonusData");

        foreach (var item in allBonusData)
        {
            if (item.CanBeUpgraded)
                yield return new ProgressionBonusData(item.BonusInfo.ID, 1);
        }
    }

    private void FirstSave()
    {
        BonusDatas data = new BonusDatas();
        data.Datas.AddRange(GetUpgradableBonuses());
        data.Datas.Sort((x, y) => x.ID.CompareTo(y.ID));
        SaveData(data);
    }

    private bool NewBonusesWasAdded()
    {
        bool dataWasChanged = false;

        foreach (var bonus in GetUpgradableBonuses())
        {
            if (!_data.Datas.Exists(data => data.ID == bonus.ID))
            {
                _data.Datas.Add(bonus);
                dataWasChanged = true;
            }
        }

        _data.Datas.Sort((x, y) => x.ID.CompareTo(y.ID));

        return dataWasChanged;
    }

    public void SaveData(BonusDatas data)
    {
        _storageService.Save(Key, data, success =>
        {
            if (success)
            {
                _data = data;
                Debug.Log("Data saved successfully!");
            }
            else
            {
                Debug.LogError("Failed to save data.");
            }
        });
    }

    public ProgressionBonusData GetDataFieldFromID(int id)
    {
        GetData();

        return _data.Datas.Find(data => data.ID == id);
    }

    public BonusDatas GetData()
    {
        if (_data == null)
            LoadData();

        return _data;
    }
}

[Serializable]
public class BonusDatas
{
    public List<ProgressionBonusData> Datas = new List<ProgressionBonusData>();
}

public class ProgressionBonusData
{
    public int ID { get; private set; }
    public int Level { get; private set; }

    public ProgressionBonusData(int id, int level)
    {
        ID = id;
        Level = level;
    }

    public void SetLevel(int level)
    {
        if (level <= 0)
            level = 1;

        Level = level;
    }
}
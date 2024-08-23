using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnetimeBonusProgression : MonoBehaviour
{
    public const string Key = "OnetimeBonusData";

    private IStorageService _storageService;
    private OnetimeBonusDatas _data;

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

        _storageService.Load<OnetimeBonusDatas>(Key, data =>
        {
            if (data == null)
                FirstSave();

            else
            {
                _data = data;
                needToSave = NewBonusesWasAdded();
            }
        });

        if (needToSave)
            SaveData(_data);
    }

    private IEnumerable<OnetimeBonusData> GetAvailableBonuses()
    {
        var allBonusData = Resources.LoadAll<BonusData>("Data/OnetimeBonusData");

        foreach (var item in allBonusData)
            yield return new OnetimeBonusData(item.BonusInfo.ID, 1);
    }

    private void FirstSave()
    {
        OnetimeBonusDatas data = new OnetimeBonusDatas();

        data.Datas.AddRange(GetAvailableBonuses());
        data.Datas.Sort((x, y) => x.ID.CompareTo(y.ID));
        SaveData(data);
    }

    private bool NewBonusesWasAdded()
    {
        bool dataWasChanged = false;

        foreach (var bonus in GetAvailableBonuses())
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

    public void SaveData(OnetimeBonusDatas data)
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

    public OnetimeBonusData GetDataFieldFromID(int id)
    {
        GetData();

        return _data.Datas.Find(data => data.ID == id);
    }

    public OnetimeBonusDatas GetData()
    {
        if (_data == null)
            LoadData();

        return _data;
    }
}

public class OnetimeBonusDatas
{
    public List<OnetimeBonusData> Datas = new List<OnetimeBonusData>();
}

public class OnetimeBonusData
{
    public int ID { get; private set; }
    public int Count { get; private set; }

    public OnetimeBonusData(int id, int count)
    {
        ID = id;
        Count = count;
    }

    public void AddCount(int count)
    {
        if (count <= 0)
            return;

        Count += count;
    }

    public void ReduceCount(int count)
    {
        if (count <= 0)
            return;

        Count -= count;
    }
}
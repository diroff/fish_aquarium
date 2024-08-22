using UnityEngine;
using UnityEngine.Events;

public class FoodStorageLoader : MonoBehaviour
{
    public const string Key = "Food";

    private IStorageService _storageService;
    private FoodData _data;

    public UnityAction<int, int> FoodCountChanged;
    public UnityAction OnStorageServiceCreated;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();
        OnStorageServiceCreated?.Invoke();

        Load();
    }

    public void Load()
    {
        if (_data != null)
            return;

        _storageService.Load<FoodData>(Key, data =>
        {
            if (data == null)
                FirstSave();

            _data = data;

            if (_data != null)
                FoodCountChanged?.Invoke(_data.FoodCount, _data.FoodCount);
        });
    }

    private void FirstSave()
    {
        FoodData data = new FoodData();
        _storageService.Save(Key, data);
    }

    public void AddMoney(int count)
    {
        var data = GetData();

        int previousFood = data.FoodCount;
        data.FoodCount += count;

        FoodCountChanged?.Invoke(previousFood, data.FoodCount);
        _storageService.Save(Key, data);
    }

    public void ReduceMoney(int count)
    {
        var data = GetData();

        if (count > data.FoodCount)
            return;

        int previousFood = data.FoodCount;

        data.FoodCount -= count;
        FoodCountChanged?.Invoke(previousFood, data.FoodCount);
        _storageService.Save(Key, data);
    }

    public FoodData GetData()
    {
        return _data;
    }
}

public class FoodData
{
    public int FoodCount;
}

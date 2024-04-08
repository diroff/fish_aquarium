using UnityEngine;
using UnityEngine.Events;

public class FoodStorageLoader : MonoBehaviour
{
    public const string Key = "Food";

    private IStorageService _storageService;
    private FoodData _food;

    public UnityAction<int, int> FoodCountChanged;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<FoodData>(Key, data =>
        {
            if (data == default)
                FirstSave();

            Load();
        });
    }

    public void Load()
    {
        _storageService.Load<FoodData>(Key, data =>
        {
            _food = data;
            FoodCountChanged?.Invoke(data.FoodCount, data.FoodCount);
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
        Load();
        return _food;
    }
}

public class FoodData
{
    public int FoodCount;
}
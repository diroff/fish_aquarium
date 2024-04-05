using UnityEngine;

public class FoodStorageLoader : MonoBehaviour
{
    public const string Key = "Food";

    private IStorageService _storageService;
    private FoodData _food;

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

        data.AddFood(count);
        _storageService.Save(Key, data);
    }

    public void ReduceMoney(int count)
    {
        var data = GetData();

        data.ReduceFood(count);
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
    public int FoodCount { get; private set; }

    public void AddFood(int count)
    {
        if (count <= 0)
            return;

        FoodCount += count;
    }

    public void ReduceFood(int count)
    {
        if (count <= FoodCount)
            return;

        FoodCount -= count;
    }
}

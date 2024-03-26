using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    [SerializeField] private string _defaulLevelName;

    public const string Key = "LevelProgression";

    private IStorageService _storageService;
    private LevelProgress _levelData;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<LevelProgress>(Key, data =>
        {
            if (data == default)
                FirstSave();

            Load();
        });
    }

    public void Load()
    {
        _storageService.Load<LevelProgress>(Key, data =>
        {
            _levelData = data;
        });
    }

    public void FirstSave()
    {
        Save(_defaulLevelName);
    }

    public void Save(string levelName)
    {
        LevelProgress data = new LevelProgress();
        data.LevelName = levelName;

        _storageService.Save(Key, data);
    }

    public LevelProgress GetData()
    {
        Load();
        return _levelData;
    }
}

public class LevelProgress
{
    public string LevelName;
}
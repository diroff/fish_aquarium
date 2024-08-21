using System.Collections.Generic;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    [SerializeField] private string _defaultLevelName = "Level1";

    public const string Key = "LevelProgression";

    private IStorageService _storageService;
    private LevelProgress _levelData;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<LevelProgress>(Key, data =>
        {
            if (data == null || data.AvailableLevels.Count == 0)
                FirstSave();
            else
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
        LevelProgress data = new LevelProgress();
        data.LastLevelName = _defaultLevelName;
        data.AvailableLevels.Add(_defaultLevelName);

        _storageService.Save(Key, data);
    }

    public void Save(string levelName)
    {
        _levelData.LastLevelName = levelName;

        if (!_levelData.AvailableLevels.Contains(levelName))
            _levelData.AvailableLevels.Add(levelName);

        _storageService.Save(Key, _levelData);
    }

    public LevelProgress GetData()
    {
        Load();
        return _levelData;
    }
}

public class LevelProgress
{
    public string LastLevelName;
    public List<string> AvailableLevels = new List<string>();
}
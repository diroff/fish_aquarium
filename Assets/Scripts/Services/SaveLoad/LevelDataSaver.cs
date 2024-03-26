using UnityEngine;

public class LevelDataSaver : MonoBehaviour
{
    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private LevelProgression _levelData;
    [SerializeField] private Level _level;

    private void OnEnable()
    {
        _level.LevelCompleted += SaveLevelData;
    }

    private void OnDisable()
    {
        _level.LevelCompleted -= SaveLevelData;
    }

    private void SaveLevelData()
    {
        _levelData.Save(_levelLoading.SceneName);
    }
}
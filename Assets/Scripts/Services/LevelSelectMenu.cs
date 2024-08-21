using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] private Transform _levelContainer;
    [SerializeField] private UILevelLoaderButton _levelButtonPrefab;

    [SerializeField] private LevelProgression _levelProgression;
    [SerializeField] private LevelLoading _levelLoading;

    private void Start()
    {
        GenerateLevelButtons();
    }

    private void GenerateLevelButtons()
    {
        LevelProgress progressData = _levelProgression.GetData();

        foreach (var levelName in progressData.AvailableLevels)
        {
            var button = Instantiate(_levelButtonPrefab, _levelContainer);

            button.Initialize(levelName, () => LoadLevel(levelName));
        }
    }

    private void LoadLevel(string levelName)
    {
        _levelLoading.LoadLevel(levelName);
    }
}
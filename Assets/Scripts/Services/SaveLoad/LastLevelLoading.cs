using UnityEngine;

public class LastLevelLoading : MonoBehaviour
{
    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private LevelProgression _progression;

    public void LoadLastLevel()
    {
        var levelName = _progression.GetData().LevelName;
        _levelLoading.LoadLevel(levelName);
    }
}
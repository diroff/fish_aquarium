using UnityEngine;

public class LastLevelLoading : MonoBehaviour
{
    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private LevelProgression _progression;

    public void LoadLastLevel()
    {
        var levelName = _progression.GetData().LastLevelName;
        _levelLoading.LoadLevel(levelName);
    }
}
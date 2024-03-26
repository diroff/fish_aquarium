using UnityEngine;

public class UILevelOverPanel : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private LevelTime _time;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _level.LevelCompleted += ShowPanel;
    }

    private void OnDisable()
    {
        _level.LevelCompleted -= ShowPanel;
    }

    private void ShowPanel()
    {
        _time.StopTime();
        _panel.SetActive(true);
    }
}
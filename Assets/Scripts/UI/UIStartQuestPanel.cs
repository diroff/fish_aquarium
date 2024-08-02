using UnityEngine;

public class UIStartQuestPanel : MonoBehaviour
{
    [SerializeField] private GameObject _questPanel;
    [SerializeField] private GameObject _questPanelPlaceholder;

    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _startGameButton;

    [SerializeField] private LevelTime _levelTime;
    [SerializeField] private Level _level;

    [SerializeField] private float _base;
    [SerializeField] private float _forLevel;

    private RectTransform _rectTransform;

    private void Start()
    {
        _levelTime.StopTime();
        _pauseButton.SetActive(false);
        _rectTransform = _questPanelPlaceholder.GetComponent<RectTransform>();
        SetHeight();
    }

    [ContextMenu("Resize")]
    public void SetHeight()
    {
        float newHeight = _base + (_forLevel * _level.Quests.Count);
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, newHeight);
    }

    public void StartGame()
    {
        _levelTime.StartTime();
        _pauseButton.SetActive(true);
        _startGameButton.SetActive(false);
    }
}

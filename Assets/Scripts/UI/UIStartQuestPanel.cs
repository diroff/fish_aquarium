using UnityEngine;

public class UIStartQuestPanel : MonoBehaviour
{
    [SerializeField] private GameObject _questPanel;
    [SerializeField] private GameObject _questPanelPlaceholder;

    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _startGameButton;

    [SerializeField] private LevelTime _levelTime;
    [SerializeField] private Level _level;

    private RectTransform _rectTransform;

    private void Start()
    {
        _levelTime.StopTime();
        _pauseButton.SetActive(false);
        _rectTransform = _questPanel.GetComponent<RectTransform>();

        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, (80 * _level.Quests.Count) + 100);
    }

    public void StartGame()
    {
        _levelTime.StartTime();
        _pauseButton.SetActive(true);
        _startGameButton.SetActive(false);
        _questPanel.transform.SetParent(_questPanelPlaceholder.transform);


        _rectTransform.anchoredPosition = Vector3.zero;
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, (90 * _level.Quests.Count));
    }
}

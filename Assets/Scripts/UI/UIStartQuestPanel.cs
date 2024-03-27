using UnityEngine;

public class UIStartQuestPanel : MonoBehaviour
{
    [SerializeField] private GameObject _questPanel;
    [SerializeField] private GameObject _questPanelPlaceholder;

    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _startGameButton;

    [SerializeField] private LevelTime _levelTime;
    [SerializeField] private Level _level;

    private void Start()
    {
        _levelTime.StopTime();
        _pauseButton.SetActive(false);
    }

    public void StartGame()
    {
        _levelTime.StartTime();
        _pauseButton.SetActive(true);
        _startGameButton.SetActive(false);
        _questPanel.transform.SetParent(_questPanelPlaceholder.transform);

        var rectTransform = _questPanel.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 120 + (50 * _level.Quests.Count));
    }
}

using UnityEngine;

public class UIQuestPanelSetup : MonoBehaviour
{
    [SerializeField] private GameObject _questPanelPlaceholder;

    [SerializeField] private Level _level;

    [SerializeField] private float _base; // 50
    [SerializeField] private float _forLevel; // 150

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = _questPanelPlaceholder.GetComponent<RectTransform>();
    }

    private void Start()
    {
        SetHeight();
    }

    [ContextMenu("Resize")]
    public void SetHeight()
    {
        float newHeight = _base + (_forLevel * _level.Quests.Count);
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, newHeight);
    }
}
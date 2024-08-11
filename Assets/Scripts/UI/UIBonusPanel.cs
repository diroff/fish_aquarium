using UnityEngine;

public class UIBonusPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _panelPlacement;

    [SerializeField] private float _sizeForItem;

    private float _currentItemCount;

    public RectTransform PanelPlacement => _panelPlacement;

    private void Start()
    {
        ResizePanel();
    }

    public void AddItem()
    {
        _currentItemCount++;
        ResizePanel();
    }

    public void RemoveItem()
    {
        _currentItemCount--;
        ResizePanel();
    }

    private void ResizePanel()
    {
        if (_currentItemCount == 0)
        {
            _panelPlacement.sizeDelta = new Vector2(_panelPlacement.sizeDelta.x, 0f);
            return;
        }

        float newHeight = _sizeForItem * _currentItemCount;
        _panelPlacement.sizeDelta = new Vector2(_panelPlacement.sizeDelta.x, newHeight);
    }
}
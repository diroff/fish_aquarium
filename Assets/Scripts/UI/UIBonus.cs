using UnityEngine;
using UnityEngine.UI;

public class UIBonus : MonoBehaviour
{
    [SerializeField] private Image _iconPlacement;
    [SerializeField] private Slider _slider;

    public void SetIcon(Sprite sprite)
    {
        _iconPlacement.sprite = sprite;
    }

    public void SetIndicatorValue(float currentTime, float bonusTime)
    {
        _slider.value = currentTime / bonusTime;
    }
}
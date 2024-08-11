using UnityEngine;
using UnityEngine.UI;

public class UIBonus : MonoBehaviour
{
    [SerializeField] private Image _iconPlacement;
    [SerializeField] private Image _slider;

    public void SetIcon(Sprite sprite)
    {
        _iconPlacement.sprite = sprite;
    }

    public void SetIndicatorValue(float currentTime, float bonusTime)
    {
        _slider.fillAmount = currentTime / bonusTime;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOnetimeBonus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bonusCountText;
    [SerializeField] private Button _bonusButton;

    [SerializeField] private OnetimeBonus _bonus;

    private void OnEnable()
    {
        _bonus.BonusCountWasChanged += OnBonusCountChanged;
    }

    private void OnDisable()
    {
        _bonus.BonusCountWasChanged -= OnBonusCountChanged;
    }

    private void OnBonusCountChanged(int count)
    {
        _bonusCountText.text = count.ToString();

        bool buttonEnabled = count > 0;
        _bonusButton.enabled = buttonEnabled;
    }
}
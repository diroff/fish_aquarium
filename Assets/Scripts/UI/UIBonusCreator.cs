using UnityEngine;

public class UIBonusCreator : MonoBehaviour
{
    [SerializeField] private Bonus _bonus;
    [SerializeField] private UIBonus _bonusIndicatorPrefab;
    [SerializeField] private GameObject _uiPlacement;

    private UIBonus _indicator;

    private void OnEnable()
    {
        _bonus.BonusStarted.AddListener(CreateBonusUI);
        _bonus.BonusEnded.AddListener(() => Destroy(_indicator.gameObject));
        _bonus.BonusTimeChanged += (OnBonusTimeChanged);
    }

    private void OnDisable()
    {
        _bonus.BonusStarted.RemoveListener(CreateBonusUI);
        _bonus.BonusEnded.RemoveListener(() => Destroy(_indicator.gameObject));
        _bonus.BonusTimeChanged -= (OnBonusTimeChanged);
    }

    private void CreateBonusUI()
    {
        _indicator = Instantiate(_bonusIndicatorPrefab, _uiPlacement.transform);
        _indicator.SetIcon(_bonus.BonusData.BonusIcon);
    }

    private void OnBonusTimeChanged(float timeLeft, float bonusTime)
    {
        _indicator.SetIndicatorValue(timeLeft, bonusTime);
    }
}
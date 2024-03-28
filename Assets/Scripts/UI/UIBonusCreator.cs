using UnityEngine;

public class UIBonusCreator : MonoBehaviour
{
    [SerializeField] private Bonus _bonus;
    [SerializeField] private UIBonus _bonusIndicatorPrefab;
    [SerializeField] private GameObject _uiStartPlacement;

    private UIBonus _indicator;
    private GameObject _uiPlacement;

    private void Awake()
    {
        if (_uiStartPlacement == null)
            return;

        _uiPlacement = _uiStartPlacement;
    }

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

    public void SetPlacement(GameObject placement)
    {
        _uiPlacement = placement;
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
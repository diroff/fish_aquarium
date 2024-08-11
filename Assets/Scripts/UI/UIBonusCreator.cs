using UnityEngine;

public class UIBonusCreator : MonoBehaviour
{
    [SerializeField] private Bonus _bonus;
    [SerializeField] private UIBonus _bonusIndicatorPrefab;
    [SerializeField] private UIBonusPanel _uiStartPlacement;

    private UIBonus _indicator;
    private UIBonusPanel _uiPlacement;

    private void Awake()
    {
        if (_uiStartPlacement == null)
            return;

        _uiPlacement = _uiStartPlacement;
    }

    private void OnEnable()
    {
        _bonus.BonusStarted.AddListener(CreateBonusUI);
        _bonus.BonusEnded.AddListener(OnBonusEnded);
        _bonus.BonusTimeChanged += (OnBonusTimeChanged);
    }

    private void OnDisable()
    {
        _bonus.BonusStarted.RemoveListener(CreateBonusUI);
        _bonus.BonusEnded.RemoveListener(OnBonusEnded);
        _bonus.BonusTimeChanged -= (OnBonusTimeChanged);
    }

    public void SetPlacement(UIBonusPanel placement)
    {
        _uiPlacement = placement;
    }

    private void CreateBonusUI()
    {
        _indicator = Instantiate(_bonusIndicatorPrefab, _uiPlacement.PanelPlacement.transform);
        _indicator.SetIcon(_bonus.BonusData.BonusIcon);
        _uiPlacement.AddItem();
    }

    private void OnBonusEnded()
    {
        _uiPlacement.RemoveItem();
        Destroy(_indicator.gameObject);
    }

    private void OnBonusTimeChanged(float timeLeft, float bonusTime)
    {
        _indicator.SetIndicatorValue(timeLeft, bonusTime);
    }
}
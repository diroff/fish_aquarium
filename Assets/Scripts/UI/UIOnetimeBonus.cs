using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOnetimeBonus : MonoBehaviour
{
    [SerializeField] private Image _bonusSprite;
    [SerializeField] private Image _blockerSprite;

    [SerializeField] private TextMeshProUGUI _bonusCountText;
    [SerializeField] private Button _bonusButton;

    [SerializeField] private OnetimeBonus _bonus;
    [SerializeField] private OnetimeBonusUsing _bonusUsing;

    private void Awake()
    {
        _bonusSprite.sprite = _bonus.BonusData.BonusIcon;
    }

    private void Start()
    {
        _bonusButton.onClick.AddListener(() => _bonusUsing.UseBonus(_bonus));
    }

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
        _blockerSprite.enabled = !buttonEnabled;
    }
}
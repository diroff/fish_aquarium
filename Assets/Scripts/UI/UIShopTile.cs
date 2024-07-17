using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _cost;

    private void OnEnable()
    {
        if (_bonusData == null)
            return;

        _bonusData.BonusWasUpdated += UpdateBonusInfo;
    }

    private void OnDisable()
    {
        if (_bonusData == null)
            return;

        _bonusData.BonusWasUpdated -= UpdateBonusInfo;
    }

    private void UpdateBonusInfo()
    {
        _name.text = _bonusData.name;
        _icon.sprite = _bonusData.BonusIcon;
        _cost.text = _bonusData.BonusInfo.TotalBonusCost().ToString();

        Debug.Log($"{_name.text} : {_cost.text}");
    }

    private BonusData _bonusData;

    public void SetData(BonusData data)
    {
        _bonusData = data;
        _bonusData.BonusWasUpdated += UpdateBonusInfo;
        UpdateBonusInfo();
    }
}
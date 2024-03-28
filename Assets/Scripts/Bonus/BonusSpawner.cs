using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _uiPanel;

    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private Vector2 _maxPosition;

    public void CreateBonus(BonusSetting bonusSetting)
    {
        var bonus = Instantiate(bonusSetting.BonusPrefab, CalculatePosition(), Quaternion.identity);
        bonus.BonusData.SetTime(bonusSetting.BonusTime);
        var creator = bonus.GetComponent<UIBonusCreator>();
        creator.SetPlacement(_uiPanel);
    }

    private Vector2 CalculatePosition()
    {
        return new Vector2(Random.Range(_minPosition.x, _maxPosition.x), Random.Range(_minPosition.y, _maxPosition.y));
    }
}

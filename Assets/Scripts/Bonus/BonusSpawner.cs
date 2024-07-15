using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private LevelEnemies _levelEnemies;

    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private Vector2 _maxPosition;

    public void CreateBonus(BonusSetting bonusSetting)
    {
        var bonus = Instantiate(bonusSetting.BonusPrefab, CalculatePosition(), Quaternion.identity);

        if (bonus is IBonusDependencies bonusWithDependencies)
        {
            bonusWithDependencies.SetObjectPool(_objectPool);
            bonusWithDependencies.SetLevelEnemies(_levelEnemies);
        }

        var creator = bonus.GetComponent<UIBonusCreator>();
        creator.SetPlacement(_uiPanel);
    }

    private Vector2 CalculatePosition()
    {
        return new Vector2(Random.Range(_minPosition.x, _maxPosition.x), Random.Range(_minPosition.y, _maxPosition.y));
    }
}

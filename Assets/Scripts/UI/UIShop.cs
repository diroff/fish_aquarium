using UnityEngine;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Transform _tileSpawnPoint;

    [SerializeField] private BonusProgressionLoading _dataLoader;
    [SerializeField] private UIShopTile _tilePrefab;

    private BonusData[] _datas;

    private void Start()
    {
        SetupShopGrid();
    }

    private void SetupShopGrid()
    {
        _datas = _dataLoader.GetCurrentBonusData();

        foreach (var item in _datas)
        {
            var tile = Instantiate(_tilePrefab, _tileSpawnPoint);
            tile.SetData(item);
        }
    }
}
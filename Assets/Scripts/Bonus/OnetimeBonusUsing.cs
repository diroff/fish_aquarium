using System.Collections.Generic;
using UnityEngine;

public class OnetimeBonusUsing : MonoBehaviour
{
    [SerializeField] private List<OnetimeBonus> _bonuses;

    [SerializeField] private LevelEnemies _enemies;
    [SerializeField] private ObjectPool _objectPool;

    [SerializeField] private Player _player;

    public List<OnetimeBonus> Bonuses => _bonuses;

    private void Awake()
    {
        SetupBonuses();
    }

    private void SetupBonuses()
    {
        foreach (var bonus in _bonuses)
        {
            bonus.SetPlayer(_player);
            bonus.SetLevelEnemies(_enemies);
            bonus.SetObjectPool(_objectPool);
        }
    }

    public void SetBonusCount(int id, int count)
    {
        foreach(var bonus in _bonuses)
        {
            if (bonus.BonusData.BonusInfo.ID != id)
                continue;

            bonus.AddAvailableBonus(count);
            break;
        }
    }

    public void UseBonus(Bonus bonus)
    {
        if (bonus.BonusWasTaked)
            return;

        bonus.UseBonus();
    }
}
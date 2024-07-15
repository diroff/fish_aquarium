using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBonuses : MonoBehaviour
{
    [SerializeField] private List<BonusSetting> _settings;
    [SerializeField] private BonusSpawner _spawner; 

    private List<BonusSetting> _bonuses = new List<BonusSetting>();
    private BonusSetting _currentBonus;

    private float _lastBonusTime = 0;

    private void Spawn(BonusSetting setting)
    {
        _spawner.CreateBonus(setting);
        _currentBonus = setting;
    }

    private void Start()
    {
        UpdateBonusList();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        if (_bonuses.Count == 0)
            UpdateBonusList();

        _currentBonus = _bonuses[0];
        yield return new WaitForSeconds(_currentBonus.TimeBeforeSpawn + _lastBonusTime);
        Spawn(_bonuses[0]);

        _lastBonusTime = _bonuses[0].BonusPrefab.BonusData.TotalBonusTime();
        _bonuses.RemoveAt(0);
        StartCoroutine(Spawn());
    }

    private void UpdateBonusList()
    {
        foreach (var setting in _settings)
        {
            _bonuses.Add(setting);
        }
    }
}

[Serializable]
public class BonusSetting
{
    [SerializeField] private Bonus _bonusPrefab;

    [SerializeField] private float _timeBeforeSpawn;
    [SerializeField] private float _timeBeforeDestroyBonus; 

    public Bonus BonusPrefab => _bonusPrefab;

    public float TimeBeforeSpawn => _timeBeforeSpawn; 
    public float TimeBeforeDestroyBonus => _timeBeforeDestroyBonus;
}
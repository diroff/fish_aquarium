using UnityEngine;

public class OnetimeBonusProgressionSceneLoader : MonoBehaviour
{
    [SerializeField] private OnetimeBonusUsing _bonusUsing;
    [SerializeField] private OnetimeBonusProgression _progression;

    private void OnEnable()
    {
        foreach (var item in _bonusUsing.Bonuses)
        {
            item.BonusCountWasChanged += OnBonusesCountChanged;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _bonusUsing.Bonuses)
        {
            item.BonusCountWasChanged -= OnBonusesCountChanged;
        }
    }

    private void Start()
    {
        SetupBonuses();
    }

    private void OnBonusesCountChanged(int id, int count)
    {
        var data = _progression.GetData();

        bool dataWasChanged = false;

        foreach (var item in data.Datas)
        {
            if (item.ID != id)
                continue;

            if (item.Count == count)
                break;

            item.SetCount(count);

            dataWasChanged = true;
        }

        if (dataWasChanged)
            _progression.SaveData(data);
    }

    private void SetupBonuses()
    {
        var data = _progression.GetData();

        foreach (var bonus in data.Datas)
            _bonusUsing.SetBonusCount(bonus.ID, bonus.Count);
    }
}
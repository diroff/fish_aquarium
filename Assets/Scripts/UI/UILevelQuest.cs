using UnityEngine;

public class UILevelQuest : MonoBehaviour
{
    [SerializeField] private UIQuest _questPrefab;
    [SerializeField] private UIQuantitativeQuest _quantitativeQuestPrefab;

    [SerializeField] private Level _level;
    [SerializeField] private GameObject _questPanel;

    private void Awake()
    {
        PrepareQuests();
    }

    private void PrepareQuests()
    {
        foreach (var quest in _level.Quests)
        {
            if(quest is QuantitativeQuest)
                PrepareQuantitativeQuest(quest);
            else
                PrepareSimpleQuest(quest);
        }
    }

    private void PrepareSimpleQuest(Quest quest)
    {
        var uiQuest = Instantiate(_questPrefab, _questPanel.transform);
        uiQuest.SetQuest(quest);
    }

    private void PrepareQuantitativeQuest(Quest quest)
    {
        var uiQuest = Instantiate(_quantitativeQuestPrefab, _questPanel.transform);
        uiQuest.SetQuest(quest);
    }
}
using UnityEngine;

public class UILevelQuest : MonoBehaviour
{
    [SerializeField] private UIQuest _questPrefab;
    [SerializeField] private Level _level;
    [SerializeField] private GameObject _questPanel;

    private void Start()
    {
        PrepareQuests();
    }

    private void PrepareQuests()
    {
        foreach (var quest in _level.Quests)
        {
            var uiQuest = Instantiate(_questPrefab, _questPanel.transform);
            uiQuest.SetQuest(quest);
        }
    }
}
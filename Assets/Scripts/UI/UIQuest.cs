using TMPro;
using UnityEngine;

public class UIQuest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questName;

    private Quest _quest;
    private bool _isCompleted = false;

    private void OnEnable()
    {
        if (_quest == null)
            return;

        _quest.QuestCompleted += OnQuestCompleted;
    }

    private void OnDisable()
    {
        if (_quest == null)
            return;

        _quest.QuestCompleted -= OnQuestCompleted;
    }

    public void SetQuest(Quest quest)
    {
        _quest = quest;
        _questName.text = quest.gameObject.name;
        _quest.QuestCompleted += OnQuestCompleted;
    }

    private void OnQuestCompleted()
    {
        //later

        _questName.fontStyle = FontStyles.Strikethrough;
        _isCompleted = true;
    }
}
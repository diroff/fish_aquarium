using TMPro;
using UnityEngine;

public class UIQuest : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI QuestName;

    protected Quest Quest;
    protected bool IsCompleted = false;

    protected virtual void OnEnable()
    {
        if (Quest == null)
            return;

        Quest.QuestCompleted += OnQuestCompleted;
    }

    protected virtual void OnDisable()
    {
        if (Quest == null)
            return;

        Quest.QuestCompleted -= OnQuestCompleted;
    }

    public virtual void SetQuest(Quest quest)
    {
        Quest = quest;
        QuestName.text = quest.gameObject.name;
        Quest.QuestCompleted += OnQuestCompleted;
    }

    private void OnQuestCompleted()
    {
        //later

        QuestName.fontStyle = FontStyles.Strikethrough;
        IsCompleted = true;
    }
}
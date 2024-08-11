using UnityEngine;
using UnityEngine.UI;

public class UIQuest : MonoBehaviour
{
    [SerializeField] protected Image QuestIcon;

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
        QuestIcon.sprite = quest.Icon;
        QuestIcon.preserveAspect = true;
        Quest.QuestCompleted += OnQuestCompleted;
    }

    private void OnQuestCompleted()
    {
        //later
        IsCompleted = true;
    }
}
using UnityEngine;
using UnityEngine.Events;

public abstract class Quest : MonoBehaviour
{
    [SerializeField] private string _questGoal;

    protected bool IsQuestFinished = false;

    public string QuestGoal => _questGoal;

    public UnityAction QuestCompleted;

    public virtual void CompleteQuest()
    {
        IsQuestFinished = true;
        QuestCompleted?.Invoke();
    }
}
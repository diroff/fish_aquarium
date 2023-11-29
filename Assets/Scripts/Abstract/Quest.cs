using UnityEngine;
using UnityEngine.Events;

public abstract class Quest : MonoBehaviour
{
    protected bool IsQuestFinished = false;

    public UnityAction QuestCompleted;

    public virtual void CompleteQuest()
    {
        QuestCompleted?.Invoke();
        Debug.Log("Quest finished!");
        IsQuestFinished = true;
    }
}
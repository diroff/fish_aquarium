using UnityEngine;
using UnityEngine.Events;

public abstract class Quest : MonoBehaviour
{
    public UnityAction QuestCompleted;

    public virtual void CompleteQuest()
    {
        QuestCompleted?.Invoke();
    }
}
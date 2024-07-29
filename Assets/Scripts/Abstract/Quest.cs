using UnityEngine;
using UnityEngine.Events;

public abstract class Quest : MonoBehaviour
{
    [SerializeField] private string _questGoal;
    [SerializeField] private Sprite _icon;

    protected bool IsQuestFinished = false;

    public string QuestGoal => _questGoal;
    public Sprite Icon => _icon;

    public UnityAction QuestCompleted;

    public virtual void CompleteQuest()
    {
        IsQuestFinished = true;
        QuestCompleted?.Invoke();
    }
}
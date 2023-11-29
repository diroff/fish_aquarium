using UnityEngine;
using UnityEngine.Events;

public abstract class QuantitativeQuest : Quest
{
    [SerializeField] private int _requiredCount;

    public UnityAction<int, int> QuestCountChanged;

    private int _currentCount;

    public void AddCount(int value)
    {
        if (IsQuestFinished)
            return;

        _currentCount += value;
        QuestCountChanged?.Invoke(_currentCount, _requiredCount);

        if (_currentCount >= _requiredCount)
            CompleteQuest();

        ShowProgress();
    }

    private void ShowProgress()
    {
        Debug.Log($"Current value:{_currentCount}/{_requiredCount}");
    }
}
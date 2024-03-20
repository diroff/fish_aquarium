using UnityEngine;
using UnityEngine.Events;

public abstract class QuantitativeQuest : Quest
{
    [SerializeField] private int _requiredCount;

    private int _currentCount;

    public int RequiredCount => _requiredCount;

    public UnityAction<int, int> QuestCountChanged;

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

    public void ResetProgress()
    {
        _currentCount = 0;
        QuestCountChanged?.Invoke(_currentCount, _requiredCount);
    }

    private void ShowProgress()
    {
        Debug.Log($"Current value:{_currentCount}/{_requiredCount}");
    }
}
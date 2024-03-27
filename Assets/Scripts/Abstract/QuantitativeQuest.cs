using UnityEngine;
using UnityEngine.Events;

public abstract class QuantitativeQuest : Quest
{
    [SerializeField] private int _requiredCount;

    private int _currentCount;

    public int RequiredCount => _requiredCount;

    public UnityAction<int, int> QuestCountChanged;

    private void Start()
    {
        QuestCountChanged?.Invoke(_currentCount, _requiredCount);
    }

    public void AddCount(int value)
    {
        if (IsQuestFinished)
            return;

        _currentCount += value;
        QuestCountChanged?.Invoke(_currentCount, _requiredCount);

        if (_currentCount >= _requiredCount)
            CompleteQuest();
    }

    public void ResetProgress()
    {
        _currentCount = 0;
        QuestCountChanged?.Invoke(_currentCount, _requiredCount);
    }
}
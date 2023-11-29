using UnityEngine;
using UnityEngine.Events;

public abstract class QuantitativeQuest : Quest
{
    [SerializeField] private int _requiredCount;

    public UnityAction<int, int> QuestCountChanged;

    private int _currentCount;

    public void AddCount(int value)
    {
        _currentCount += value;
        QuestCountChanged?.Invoke(_currentCount, _requiredCount);

        if (_currentCount >= _requiredCount)
            CompleteQuest();
    }
}
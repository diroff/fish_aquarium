using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    private List<Quest> _quests;
    private int _questsCompleted;

    public UnityAction LevelCompleted;

    private void Awake()
    {
        _quests = GetComponentsInChildren<Quest>().ToList();

        if (_quests.Count == 0)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        foreach (Quest _quest in _quests)
            _quest.QuestCompleted += QuestCountRemainedChecker;
    }

    private void OnDisable()
    {
        foreach (Quest _quest in _quests)
            _quest.QuestCompleted -= QuestCountRemainedChecker;
    }

    private void QuestCountRemainedChecker() 
    {
        _questsCompleted++;

        Debug.Log($"Quest completed:{_questsCompleted}/{_quests.Count}");

        if (_questsCompleted == _quests.Count)
            FinishLevel();
    }

    private void FinishLevel()
    {
        LevelCompleted?.Invoke();
        Debug.Log("Level completed");
    }
}
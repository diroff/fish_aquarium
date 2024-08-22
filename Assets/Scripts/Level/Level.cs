using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Quest> _quests;
    [SerializeField] private LevelTime _levelTime;

    private int _questsCompleted;

    public List<Quest> Quests => _quests;

    public UnityAction LevelStarted;
    public UnityAction LevelCompleted;

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

    private void Start()
    {
        _levelTime.StopTime();
    }

    public void StartLevel()
    {
        _levelTime.StartTime();
        LevelStarted?.Invoke();
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
        _levelTime.StopTime();
        LevelCompleted?.Invoke();
        Debug.Log("Level completed");
    }
}
using TMPro;
using UnityEngine;

public class UIQuantitativeQuest : UIQuest
{
    [SerializeField] private TextMeshProUGUI _progressCountText;

    private QuantitativeQuest _quest;

    protected override void OnEnable()
    {
        base.OnEnable();

        if(_quest != null )
            _quest.QuestCountChanged += OnQuestCountChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_quest != null)
            _quest.QuestCountChanged -= OnQuestCountChanged;
    }

    public override void SetQuest(Quest quest)
    {
        base.SetQuest(quest);
        _quest = quest as QuantitativeQuest;

        _quest.QuestCountChanged += OnQuestCountChanged;
    }

    private void OnQuestCountChanged(int currentCount, int requiredCount)
    {
        if (IsCompleted)
        {
            _progressCountText.text = requiredCount + "/" + requiredCount;
            return;
        }

        _progressCountText.text = currentCount + "/" + requiredCount;
    }
}
using System.Collections;
using UnityEngine;

public class SurviveForTimeQuest : QuantitativeQuest
{
    private float _requiredTime;
    private float _currentTime;

    private void OnEnable()
    {
        _requiredTime = RequiredCount;
        StartCoroutine(TimeChecker());
    }

    private IEnumerator TimeChecker()
    {
        while (_currentTime < _requiredTime)
        {
            int previousTime = (int)_currentTime;
            _currentTime += Time.deltaTime;
            AddCount((int)_currentTime - previousTime);
            yield return null;
        }

        CompleteQuest();
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "New BonusData", order = 51)]
public class BonusData : ScriptableObject
{
    [SerializeField] private float _bonusTime;
    [SerializeField] private float _timeBeforeDestroying = 5f;
    [SerializeField] private Sprite _bonusIcon;

    public float BonusTime => _bonusTime;
    public float TimeBeforeDestroying => _timeBeforeDestroying;
    public Sprite BonusIcon => _bonusIcon;

    public void SetTime(float time)
    {
        if (time <= 0)
        {
            Debug.LogError("Bonus time less 0");
            return;
        }

        _bonusTime = time;
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "New BonusData", order = 51)]
public class BonusData : ScriptableObject
{
    [SerializeField] private float _bonusTime;
    [SerializeField] private Sprite _bonusIcon;

    public float BonusTime => _bonusTime;
    public Sprite BonusIcon => _bonusIcon;
}
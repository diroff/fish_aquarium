using UnityEngine;

public class BonusData : MonoBehaviour
{
    [SerializeField] private float _bonusTime;
    [SerializeField] private Sprite _bonusIcon;

    public float BonusTime => _bonusTime;
    public Sprite BonusIcon => _bonusIcon;
}
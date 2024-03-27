using TMPro;
using UnityEngine;

public class CreatureLevelDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textField;
    [SerializeField] private Creature _creature;

    private void OnEnable()
    {
        _creature.LevelChanged += UpdateLevel;
    }

    private void OnDisable()
    {
        _creature.LevelChanged -= UpdateLevel;
    }

    private void UpdateLevel(int previousLevel, int currentLevel)
    {
        _textField.text = currentLevel.ToString();
    }
}

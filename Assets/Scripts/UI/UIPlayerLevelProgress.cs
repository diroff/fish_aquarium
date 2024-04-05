using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerLevelProgress : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _currentExperienceCount;
    [SerializeField] private TextMeshProUGUI _requiredExperienceCount;

    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _player.ExperienceWasChanged += OnPlayerExperienceChanged;
    }

    private void OnDisable()
    {
        _player.ExperienceWasChanged -= OnPlayerExperienceChanged;
    }

    private void OnPlayerExperienceChanged(int currentExperience, int requiredExperience)
    {
        _panel.SetActive(true);
        _currentExperienceCount.text = currentExperience.ToString();
        _requiredExperienceCount.text = requiredExperience.ToString();

        _slider.value = (float)currentExperience / requiredExperience;
    }
}
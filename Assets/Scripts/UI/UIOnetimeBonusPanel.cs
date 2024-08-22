using UnityEngine;

public class UIOnetimeBonusPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Level _level;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _level.LevelCompleted += OnLevelCompleted;
        _level.LevelStarted += OnLevelStarted;

        _player.Died += OnPlayerWasDied;
        _player.WasRespawn += OnPlayerWasRespawn;
    }

    private void OnDisable()
    {
        _level.LevelCompleted -= OnLevelCompleted;
        _level.LevelStarted -= OnLevelStarted;

        _player.Died -= OnPlayerWasDied;
        _player.WasRespawn -= OnPlayerWasRespawn;
    }

    private void SetPanelState(bool active)
    {
        _panel.SetActive(active);
    }

    private void OnLevelCompleted()
    {
        SetPanelState(false);
    }

    private void OnLevelStarted()
    {
        SetPanelState(true);
    }

    private void OnPlayerWasDied(Creature creature)
    {
        SetPanelState(false);
    }

    private void OnPlayerWasRespawn()
    {
        SetPanelState(true);
    }
}
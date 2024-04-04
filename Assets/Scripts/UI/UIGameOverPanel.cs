using UnityEngine;

public class UIGameOverPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LevelTime _time;
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied(Creature creature)
    {
        _gameOverPanel.SetActive(true);
        _time.StopTime();
    }

    public void ResumeGame()
    {
        _gameOverPanel.SetActive(false);
        _player.Respawn();
        _time.StartTime();
    }
}
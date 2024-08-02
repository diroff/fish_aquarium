using UnityEngine;

public class UIStartPanel : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private LevelTime _levelTime;

    private void Start()
    {
        _levelTime.StopTime();
        _pauseButton.SetActive(false);
    }

    public void StartGame()
    {
        _levelTime.StartTime();
        _pauseButton.SetActive(true);
        _startGameButton.SetActive(false);
    }
}
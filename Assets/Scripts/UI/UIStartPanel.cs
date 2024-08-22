using UnityEngine;

public class UIStartPanel : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private Level _level;

    private void Start()
    {
        _pauseButton.SetActive(false);
    }

    public void StartGame()
    {
        _level.StartLevel();
        _pauseButton.SetActive(true);
        _startGameButton.SetActive(false);
    }
}
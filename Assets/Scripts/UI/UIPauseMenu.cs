using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private LevelTime _time;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _pauseButton;

    public void EnablePause()
    {
        _time.StopTime();
        _pauseButton.SetActive(false);
        _panel.SetActive(true);
    }

    public void DisablePause()
    {
        _time.StartTime();
        _pauseButton.SetActive(true);
        _panel.SetActive(false);
    }
}
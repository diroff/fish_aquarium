using UnityEngine;

public class LevelTime : MonoBehaviour
{
    [SerializeField] private float _startTime = 1f;

    private void Awake()
    {
        Time.timeScale = _startTime;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        Time.timeScale = _startTime;
    }
}
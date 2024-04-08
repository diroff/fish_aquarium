using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> _panels;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private string _gameScene;

    private GameObject _currentPanel;
    private GameObject _previousPanel;

    private void Start()
    {
        _currentPanel = _startPanel;

        ClosePanels();
        _currentPanel.gameObject.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_gameScene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }

    public void OpenPanel(GameObject panel)
    {
        _previousPanel = _currentPanel;
        _currentPanel.SetActive(false);
        _currentPanel = panel;
        _currentPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        _currentPanel.SetActive(false);
        _currentPanel = _previousPanel;
        _currentPanel.SetActive(true);
    }

    private void ClosePanels()
    {
        for (int i = 0; i < _panels.Count; i++)
        {
            _panels[i].gameObject.SetActive(false);
        }
    }
}
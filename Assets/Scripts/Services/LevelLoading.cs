using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public string SceneName => _sceneName;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneName);
    }
}
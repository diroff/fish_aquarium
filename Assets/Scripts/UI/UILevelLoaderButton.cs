using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class UILevelLoaderButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private Button _button;

    public void Initialize(string name, UnityAction action)
    {
        SetupLevelName(name);
        _button.onClick.AddListener(action);
    }

    private void SetupLevelName(string levelName)
    {
        string number = Regex.Match(levelName, @"\d+").Value;

        _levelName.text = number;
    }
}
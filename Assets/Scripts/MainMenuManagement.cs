using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManagement : MonoBehaviour
{
    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _options;

    public void GoToScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void QuitProject()
    {
        Application.Quit();
    }

    public void Options()
    {
        _main.SetActive(false);
        _options.SetActive(true);
    }

    public void Back()
    {
        _main.SetActive(true);
        _options.SetActive(false);
    }
}

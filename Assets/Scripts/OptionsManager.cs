using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _handGun;
    [SerializeField] private GameObject _mute;

    private bool _isPaused = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPaused)
            {
                Resume();
            }
            else Pause();
        }
    }
    
    public void Pause()
    {
        if(AudioListener.volume>0) _mute.SetActive(false);
        _isPaused=true;
        Time.timeScale = 0f;
        _hud.SetActive(false);
        _handGun.SetActive(false);
        _optionsCanvas.SetActive(true);
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        _hud.SetActive(true);
        _handGun.SetActive(true);
        _optionsCanvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

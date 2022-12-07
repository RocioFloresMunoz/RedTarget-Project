
using System.Runtime.Serialization;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _sliderValue;
    [SerializeField] private GameObject _imageMute;

    // Start is called before the first frame update
    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("VolumenAudio");
        AudioListener.volume = _slider.value;
        isMuteActive();
    }

    public void ChangeSlider(float value)
    {
        _sliderValue = value;
        PlayerPrefs.SetFloat("VolumenAudio",_sliderValue);
        AudioListener.volume = _slider.value;
        isMuteActive();
    }
    
    public void isMuteActive()
    {
        if(_sliderValue == 0) _imageMute.SetActive(true);
        else _imageMute.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject _feedbackCanvas;
    [SerializeField] private GameObject _miniGameCanvas;
    [SerializeField] private AudioSource _audioSourceSounds;
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioClip _bellSound;
    [SerializeField] private AudioClip _miniGameMusic;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private List<GameObject> _targets;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textBestScore;
    [SerializeField] private TextMeshProUGUI _textTimer;

    [Header ("MiniGame Variables")]
    [SerializeField] private int _score;
    [SerializeField] private int _bestScore;
    [SerializeField] private float _timer = 15f;
    [SerializeField] private bool _isMiniGameRun = false;
    [SerializeField] private bool _isFirstTime = true;
    [SerializeField] private int _isFirstTimeBoolNumber = 1;

    public void Awake()
    {
        _isFirstTimeBoolNumber = PlayerPrefs.GetInt("FirstPlay");
        if(_isFirstTimeBoolNumber == 0) _isFirstTime = false;
        if(_isFirstTimeBoolNumber == 1) _isFirstTime = true;
    }

    private void Update()
    {
        TimerCount();
        _textScore.text = _score.ToString();
    }

    public void RiseRandomTarget()
    {
        int randomNumber = Random.Range(0,_targets.Count);
        _targets[randomNumber].SetActive(true);
        _targets[randomNumber].GetComponent<Animator>().SetBool("RiseBool",true);
        _targets[randomNumber].GetComponent<Animator>().SetBool("FallBool",false);

    }

    public void SetBoolStartMiniGame(bool value)
    {
        _isMiniGameRun = value;
        StartMiniGame();
    }

    public void StartMiniGame()
    {
        _score = 0;
        _bestScore = PlayerPrefs.GetInt("BestScore");
        _textBestScore.text = _bestScore.ToString();
        _feedbackCanvas.SetActive(false);
        _miniGameCanvas.SetActive(true);

        foreach(GameObject target in _targets)
        {
            target.GetComponent<Animator>().SetBool("FallBool", true);
        }

        _audioSourceSounds.clip = _bellSound;
        _audioSourceSounds.Play();

        _audioSourceMusic.clip = _miniGameMusic;
        _audioSourceMusic.Play();

        RiseRandomTarget();
    }

    public void TimerCount()
    {
        if(_isMiniGameRun)
        {
            _timer -= Time.deltaTime;
            _textTimer.text = _timer.ToString("f0");

            if(_timer < 0)
            {
                FinishMiniGame();
            }
        }        
    }

    public void SetScore(int points)
    {
        _score = _score + points;
    }

    public void FinishMiniGame()
    {
        _miniGameCanvas.SetActive(false);
        
        if(_isFirstTime)
        {
            _bestScore = _score;
            PlayerPrefs.SetInt("BestScore", _bestScore);
            
        }
        else
        {
            if(_score > _bestScore){
                _bestScore = _score;
                PlayerPrefs.SetInt("BestScore",_bestScore);
            }
            else PlayerPrefs.SetInt("BestScore",_bestScore);
        }

        foreach(GameObject target in _targets)
        {
            target.GetComponent<Animator>().SetBool("FallBool", true);
            target.GetComponent<Animator>().SetBool("RiseBool", false);
        }

        _audioSourceMusic.clip = _backgroundMusic;
        _audioSourceMusic.Play();

        _isMiniGameRun = false;
        _isFirstTime=false;
        PlayerPrefs.SetInt("FirstPlay",0);

        _timer = 15f;
        _feedbackCanvas.SetActive(true);
    }

    public bool IsMinigameRun(){
        return _isMiniGameRun;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    #region CLASS_VARIABLES
    [Header ("References")]
    private AudioSource _audioSource;
    [SerializeField] private Camera _playerCamera;
    private CharacterController _characterController;
    [SerializeField] private GameObject _feedbackCanvas;
    [SerializeField] private GameObject _rangeTestCanvas;

    [Header ("General")]
    [SerializeField] private float _gravityScale = -20f;
    private Vector3 _moveInput = Vector3.zero;
    private Vector3 _rotationInput = Vector3.zero;
    private float _cameraVerticalAngle = 0;

    [Header ("Movement")]
    [Tooltip("Variable de velocidad de movimiento")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _run = 10f;

    [Header ("Rotation")]
    [Tooltip ("Sensibilidad del mouse al rotar la cámara")]
    [SerializeField] private float _rotationSensibility = 30;

    [Header ("Jump")]
    [Tooltip ("Variable de fuerza de salto")]
    [SerializeField] private float _jumpForce = 0.9f;

    [Header ("Audio")]
    [Tooltip ("Variable para almacenar el sonido de salto")]
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _ammoboxSound;

    [Header ("Weapon")]
    [SerializeField] private SimpleShoot _weapon;

    [Header ("MiniGame")]
    [SerializeField] private MiniGameManager _miniGameManager;
    private bool _canStart = false;
    


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
        _weapon = FindObjectOfType<SimpleShoot>();

        AudioListener.volume = PlayerPrefs.GetFloat("VolumenAudio");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Look();
        StartMiniGame(); 
    }

    public void Movement()
    {
        if(_characterController.isGrounded)
        {
            _moveInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
            _moveInput = Vector3.ClampMagnitude(_moveInput,1f);
            
            if(Input.GetButton("Sprint"))
            {
                _moveInput = transform.TransformDirection(_moveInput * _run);
            }
            else
            {
                _moveInput = transform.TransformDirection(_moveInput * _speed);
            }
            Jump();
        }

        _moveInput.y += _gravityScale * Time.deltaTime;
        _characterController.Move(_moveInput * Time.deltaTime);

        
    }

    public void Jump()
    {
        if(Input.GetButton("Jump"))
        {
            _moveInput.y = Mathf.Sqrt(_jumpForce * -2 * _gravityScale);
            _audioSource.clip = _jumpSound;
            _audioSource.Play();
        }
    }

    public void Look()
    {
        _rotationInput.x = Input.GetAxis("Mouse X") * _rotationSensibility * Time.deltaTime;
        _rotationInput.y = Input.GetAxis("Mouse Y") * _rotationSensibility * Time.deltaTime;

        _cameraVerticalAngle = _cameraVerticalAngle + _rotationInput.y;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle,-70,70);

        transform.Rotate(Vector3.up * _rotationInput.x);
        _playerCamera.transform.localRotation = Quaternion.Euler(-_cameraVerticalAngle,0f,0f);
    }


    public void StartMiniGame()
    {
        if(_canStart = true && Input.GetKeyDown(KeyCode.E))
        {
            _miniGameManager.SetBoolStartMiniGame(_canStart);   
        }
    }


    // INTERSECCIONES
    public void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("BulletBox") && _weapon.BulletCountMax())
        {
            _weapon.SetBulletCount();

            _audioSource.clip = _ammoboxSound;
            _audioSource.Play();

            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Bell"))
        {
            _feedbackCanvas.SetActive(true);
            _canStart = true;
        }

        if(other.gameObject.CompareTag("RangeTest"))
        {
            _rangeTestCanvas.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _feedbackCanvas.SetActive(false);
    }
}

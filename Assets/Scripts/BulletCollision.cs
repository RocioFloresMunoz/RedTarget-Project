using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField] private  int _pointsValue;
    [SerializeField] private Animator _animator;
    private AudioSource _audioSource;
    private MiniGameManager _miniGameManager;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _miniGameManager = FindObjectOfType<MiniGameManager>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            _miniGameManager.SetScore(_pointsValue);
            _audioSource.Play();

            _animator.SetBool("FallBool", true);
            _animator.SetBool("RiseBool", false);
            _miniGameManager.RiseRandomTarget();
        }
    }

    public Animator GetAnimator()
    {
        return _animator;
    }
}

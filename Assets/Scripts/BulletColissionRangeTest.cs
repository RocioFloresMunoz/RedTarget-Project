using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletColissionRangeTest : MonoBehaviour
{
    [SerializeField] private  int _pointsValue;
    private RangeTestManager _rangeTestManager;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rangeTestManager = FindObjectOfType<RangeTestManager>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            _rangeTestManager.SetPoints(_pointsValue);
            _audioSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    [Header ("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _fireSound;

    [Header ("Bullets")]
    [SerializeField][Range(0,100)] private int _bulletCount = 15;
    [SerializeField] private TextMeshProUGUI _textBullets;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        _textBullets.text = _bulletCount.ToString();
    }

    void Update()
    {
        //If you want a different input, change it here
        if (Input.GetButtonDown("Fire1") && _bulletCount!=0)
        {
            
            //Calls animation on the gun that has the relevant animation events that will fire
            gunAnimator.SetTrigger("Fire");
        }
        _textBullets.text = _bulletCount.ToString();
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        if(_bulletCount != 0)
        {
            if (muzzleFlashPrefab)
            {
                //Create the muzzle flash
                GameObject tempFlash;
                tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

                //Destroy the muzzle flash effect
                Destroy(tempFlash, destroyTimer);
            }

            //cancels if there's no bullet prefeb
            if (!bulletPrefab)
            { return; }

            // Create a bullet and add force on it in direction of the barrel
            Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

            // Play the fire sound
            _audioSource.clip = _fireSound;
            _audioSource.Play();

            // Rest a bullet to the counter
            _bulletCount--;
        }
        else
        {
            return;
        }
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    public void SetBulletCount()
    {
        _bulletCount = _bulletCount + 15;
        if(_bulletCount > 100)
        {
            _bulletCount = 100;
        }
    }

    public bool BulletCountMax(){
        return _bulletCount < 100;
    }
}

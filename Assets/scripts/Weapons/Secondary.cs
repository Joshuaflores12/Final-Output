using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Secondary : MonoBehaviour
{
    public int currentAmmo;
    public int maxAmmo = 4;
    [SerializeField] private float reloadTime = 4.5f;
    [SerializeField] private bool isReloading = false;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI ReloadUpdater;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform BulletHolder;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip BigWeapon;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;

        Ammo.text = "Ammo: " + currentAmmo;

        ReloadUpdater.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isReloading)
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;

                Ammo.text = "Ammo: " + currentAmmo;

                Instantiate(Projectile, BulletHolder.position, Quaternion.identity);

                source.PlayOneShot(BigWeapon);

            }

            else if (currentAmmo <= 0)
            {
                ReloadUpdater.text = "Out of Ammo!";
            }
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        ReloadUpdater.text = "Reloading!";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        Ammo.text = "Ammo: " + currentAmmo;

        ReloadUpdater.text = "";

        isReloading = false;
    }

    public void OnEnable()
    {
        isReloading = false;

        ReloadUpdater.text = ""; 
    }
}

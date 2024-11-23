using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons;
    [SerializeField] private int currentWeaponList = 0;
    public TextMeshProUGUI WeaponUpdate;
    [SerializeField] private float raycastLength;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon(currentWeaponList);
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) CycleWeapons(1);

        else if(Input.GetAxis("Mouse ScrollWheel") < 0f) CycleWeapons(-1);
        RaycastDetector();
    }

    public void CycleWeapons(int direction) 
    {
        currentWeaponList = (currentWeaponList + direction + weapons.Count) % weapons.Count;

        EquipWeapon(currentWeaponList);
    }

    public void EquipWeapon(int index) 
    {
        
        for (int i = 0; i < weapons.Count; i++) 
        {
            weapons[i].SetActive(i == index);

            WeaponUpdate.text = "Currently equipped: " + weapons[index].name;

            WeaponAmmoUpdate(index);
        }

        currentWeaponList = index;
    }

    public void WeaponAmmoUpdate(int index) 
    {
        if (weapons[index].TryGetComponent(out Primary primaryWeapon))
        {
            primaryWeapon.Ammo.text = "Ammo: " + primaryWeapon.currentAmmo;
        }

        if (weapons[index].TryGetComponent(out Secondary secondaryWeapon))
        {
            secondaryWeapon.Ammo.text = "Ammo: " + secondaryWeapon.currentAmmo;
        }

        else if (weapons[index].TryGetComponent(out Grenade utiliyWeapon)) 
        {
            utiliyWeapon.Ammo.text = "" + utiliyWeapon.currentAmmo;
        }
    }

    public void RaycastDetector() 
    {
        Vector2 dir = (enemy.position - transform.position);

        raycastLength = Vector2.Distance(transform.position, enemy.position);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, raycastLength, enemyMask);

        Debug.DrawRay(transform.position, dir, Color.red);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                float enemyPosition = Vector2.Distance(player.position, hit.point);

                Debug.Log("Enemy spotted at: " + enemyPosition + "Enemy: " + hit.collider.gameObject.name);
            }
        }

        else 
        {
            Debug.Log("No enemy found: ");
        }
    }
}

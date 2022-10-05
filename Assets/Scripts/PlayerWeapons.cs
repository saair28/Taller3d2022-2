using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    PlayerShoot pShoot;
    public int weaponIndex = 0;
    public GameObject currentWeapon;
    [SerializeField] public List<GameObject> weaponList = new List<GameObject>();
    public bool isChangingWeapon = false;

    GameObject weaponPickedUp;
    int weaponNumberSearchIndex = 0;

    void Start()
    {
        pShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        ScrollToSwitchWeapon();
        NumbersToSwitchWeapon();
        if (isChangingWeapon)
        {
            UpdateWeaponStats();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Weapon") && !isChangingWeapon)
        {
            isChangingWeapon = true;
            weaponPickedUp = col.gameObject;
            CheckGun(weaponPickedUp);
        }
    }

    public void CheckGun(GameObject weapon)
    {
        bool hasWeapon = false;
        if (weaponList.Count != 0)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                if (weaponList[i].GetComponent<WeaponScript>().weaponName == weaponPickedUp.GetComponent<WeaponScript>().weaponName)
                {
                    hasWeapon = true;

                    //Debug.Log("YA TIENES ESTA ARMA");
                    currentWeapon = weaponList[i];
                    //weaponList[i].bulletsInMagazineActual = weapon.bulletsInMagazineActual;
                    //weaponList[i].bulletsInTotal = weapon.bulletsInTotal;
                    weaponIndex = i + 1;
                }
            }

            if (!hasWeapon)
            {
                //Debug.Log("NO TIENES ESTA ARMA");
                weaponList.Add(weaponPickedUp);
                weaponIndex++;
                currentWeapon = weaponPickedUp;
            }
        }
        else
        {
            weaponList.Add(weaponPickedUp);
            weaponIndex++;
            currentWeapon = weaponPickedUp;
        }

        isChangingWeapon = true;
    }

    void ChangeWeaponUp()
    {
        if (weaponIndex == 1)
        {
            weaponIndex = weaponList.Count;
        }
        else
        {
            weaponIndex--;
        }
        if (currentWeapon != null)
            currentWeapon = weaponList[weaponIndex - 1];
    }

    void ChangeWeaponDown()
    {
        if (weaponIndex == weaponList.Count)
        {
            weaponIndex = 1;
        }
        else
        {
            weaponIndex++;
        }
        if (currentWeapon != null)
            currentWeapon = weaponList[weaponIndex - 1];
    }

    void ScrollToSwitchWeapon()
    {
        if (weaponList.Count > 1)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                isChangingWeapon = true;
                ChangeWeaponUp();
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                isChangingWeapon = true;
                ChangeWeaponDown();
            }
        }

        //if (currentWeapon != null)
        //    currentWeapon = weaponList[weaponIndex - 1];
    }

    void NumbersToSwitchWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponNumberSearchIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponNumberSearchIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponNumberSearchIndex = 3;
        }

        if (weaponNumberSearchIndex != 0)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                if (weaponList[i].GetComponent<WeaponScript>().key == weaponNumberSearchIndex)
                {
                    if (currentWeapon != weaponList[i])
                    {
                        isChangingWeapon = true;
                        currentWeapon = weaponList[i];
                    }
                    break;

                }
            }
            weaponNumberSearchIndex = 0;
        }
    }

    void UpdateWeaponStats()
    {
        pShoot.shotBullet = false;
        pShoot.damage = currentWeapon.GetComponent<WeaponScript>().damage;
        pShoot.bulletSpeed = currentWeapon.GetComponent<WeaponScript>().bulletSpeed;
        pShoot.fireRate = currentWeapon.GetComponent<WeaponScript>().fireRate;
        pShoot.bulletPrefab = currentWeapon.GetComponent<WeaponScript>().bulletPrefab;
        pShoot.bulletsPerShot = currentWeapon.GetComponent<WeaponScript>().bulletsPerShot;
        pShoot.bulletRotation = currentWeapon.GetComponent<WeaponScript>().bulletRotation;
        pShoot.bulletReach = currentWeapon.GetComponent<WeaponScript>().bulletReach;
        //Debug.Log(currentWeapon.GetComponent<WeaponScript>().weaponName);
        isChangingWeapon = false;
    }
}

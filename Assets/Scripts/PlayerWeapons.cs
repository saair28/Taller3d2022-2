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

    void Start()
    {
        pShoot = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
        if(isChangingWeapon)
            ChangeWeapon();
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

                    Debug.Log("YA TIENES ESTA ARMA");
                    currentWeapon = weaponList[i];
                    //weaponList[i].bulletsInMagazineActual = weapon.bulletsInMagazineActual;
                    //weaponList[i].bulletsInTotal = weapon.bulletsInTotal;
                    weaponIndex = i + 1;
                }
            }

            if (!hasWeapon)
            {
                Debug.Log("NO TIENES ESTA ARMA");
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

        UpdateWeaponStats();
    }

    void ChangeWeapon()
    {
        if (weaponList.Count > 1)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                isChangingWeapon = true;
                if (weaponIndex == 1)
                {
                    weaponIndex = weaponList.Count;
                }
                else
                {
                    weaponIndex--;
                }
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                isChangingWeapon = true;
                if (weaponIndex == weaponList.Count)
                {
                    weaponIndex = 1;
                }
                else
                {
                    weaponIndex++;
                }
            }
        }

        if (weaponList.Count > 0)
        {
            currentWeapon = weaponList[weaponIndex - 1];
        }

        UpdateWeaponStats();
        isChangingWeapon = false;
    }

    void UpdateWeaponStats()
    {
        pShoot.bulletSpeed = currentWeapon.GetComponent<WeaponScript>().bulletSpeed;
        pShoot.fireRate = currentWeapon.GetComponent<WeaponScript>().fireRate;
        Debug.Log(currentWeapon.GetComponent<WeaponScript>().weaponName);
    }
}

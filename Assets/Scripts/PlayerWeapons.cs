using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    PlayerShoot pShoot;
    public int weaponIndex = 0;
    public GameObject currentWeapon;
    public GameObject currentWeaponPrefab;
    [SerializeField] public List<GameObject> weaponList = new List<GameObject>();
    public bool isChangingWeapon = false;

    GameObject weaponPickedUp;
    int weaponNumberSearchIndex = 0;
    public Transform weaponPosition;

    public float timeBetweenWeapons;
    float timerWeaponsCount;
    bool canSwitchWeapon = true;

    void Start()
    {
        pShoot = GetComponent<PlayerShoot>();
        timerWeaponsCount = timeBetweenWeapons;
    }

    private void Update()
    {
        if(FindObjectOfType<PauseOptionsMenu>() != null && !PauseOptionsMenu.instance.isPaused && canSwitchWeapon)
        {
            ScrollToSwitchWeapon();
            NumbersToSwitchWeapon();
        }

        if (isChangingWeapon)
        {
            UpdateWeaponStats();
        }

        if(!canSwitchWeapon)
        {
            timerWeaponsCount -= Time.deltaTime;

            if(timerWeaponsCount <= 0)
            {
                timerWeaponsCount = timeBetweenWeapons;
                canSwitchWeapon = true;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Weapon") && !isChangingWeapon)
        {
            isChangingWeapon = true;
            weaponPickedUp = col.gameObject;
            CheckGun(weaponPickedUp);
            Destroy(col);
            col.gameObject.GetComponent<MeshRenderer>().enabled = false;
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
                    UpdateWeaponModel();

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
                UpdateWeaponModel();
            }
        }
        else
        {
            weaponList.Add(weaponPickedUp);
            weaponIndex++;
            currentWeapon = weaponPickedUp;
            UpdateWeaponModel();
        }

        isChangingWeapon = true;
    }

    public void UpdateWeaponModel()
    {
        if(currentWeaponPrefab != null)
            Destroy(currentWeaponPrefab);

        GetComponent<PlayerShoot>().bulletOrigin = null;
        currentWeaponPrefab = Instantiate(currentWeapon.GetComponent<WeaponScript>().weaponPrefab, weaponPosition);
        GetComponent<PlayerShoot>().bulletOrigin = currentWeaponPrefab.GetComponentInChildren<Transform>();
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
                UpdateWeaponModel();

                canSwitchWeapon = false;
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                isChangingWeapon = true;
                ChangeWeaponDown();
                UpdateWeaponModel();

                canSwitchWeapon = false;
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
                        UpdateWeaponModel();

                    }

                    canSwitchWeapon = false;
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
        pShoot.shootSFX = currentWeapon.GetComponent<WeaponScript>().weaponSound;
        //Debug.Log(currentWeapon.GetComponent<WeaponScript>().weaponName);
        isChangingWeapon = false;
    }
}

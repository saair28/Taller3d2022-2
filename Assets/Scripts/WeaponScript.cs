using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Se pone este script en cada una de las armas y se colocan los valores que se quieran para esa arma en el inspector.
public class WeaponScript : MonoBehaviour
{
    [SerializeField] public string weaponName;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float fireRate;
    [SerializeField] public float damage;
    [SerializeField] public int key;
    [SerializeField] public int bulletsPerShot;
    [SerializeField] public float bulletRotation;
    [SerializeField] public float bulletReach;
    [SerializeField] public GameObject weaponPrefab;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform bulletOrigin;
    [SerializeField] public int bulletColor;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Camera cam;
    //[SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform bulletOrigin;
    [SerializeField] public float bulletSpeed;
    bool canShoot => (Input.GetMouseButton(0) && !shotBullet && GetComponent<PlayerWeapons>().currentWeapon != null && bulletOrigin != null);
    public bool shotBullet = false;
    [SerializeField] public float damage;
    public float damageMultiplier = 1;
    [SerializeField] public float fireRate;
    [HideInInspector] public float fireRateCount;
    [HideInInspector] public int bulletsPerShot;
    [HideInInspector] public float bulletRotation;
    [HideInInspector] public float bulletReach;
    Vector3 destination;
    public GameObject bulletPrefab;

    AudioSource sfxSource;
    [SerializeField] public AudioClip shootSFX;

    void Start()
    {
        fireRateCount = fireRate;

        sfxSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckIfShot();
        if (canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        AudioManager.instance.PlaySFX(sfxSource, shootSFX, 0.2f);

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        //Para cambiar las balas en caso se necesite
        //GameObject sgPrefab = GetComponent<PlayerWeapons>().currentWeapon.GetComponent<WeaponScript>().bulletPrefab;

        // Un solo disparo -> Pistola normal
        //var bullet = Instantiate(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
   
        //bullet.GetComponent<Rigidbody>().velocity = (destination - bulletOrigin.position).normalized * bulletSpeed;
        //Destroy(bullet, 3f);

        for(int i = 0; i < bulletsPerShot; i++)
        {
            float randomRotationX = Random.Range(-bulletRotation, bulletRotation);
            float randomRotationY = Random.Range(-bulletRotation, bulletRotation);
            float randomRotationZ = Random.Range(-bulletRotation, bulletRotation);

            Vector3 _bulletRotation = new Vector3(randomRotationX, randomRotationY, randomRotationZ);
            //Vector3 finalRotation = (destination - bulletOrigin.position).normalized;
            Vector3 finalRotation = (destination - bulletOrigin.position);

            //var bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.LookRotation(bulletOrigin.transform.forward));
            //var bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.LookRotation(finalRotation));
            var bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.LookRotation(finalRotation));
            bullet.transform.Rotate(_bulletRotation);
            //bullet.transform.Rotate(finalRotation + _bulletRotation);
            // Con esta línea de abajo, haces que las balas vayan directas al centro de la pantalla, en lugar de solo ir hacia delante.
            //bullet.GetComponent<Rigidbody>().velocity = (finalRotation + _bulletRotation).normalized * bulletSpeed;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            //bullet.GetComponent<BulletScript>().bulletDamage = GetComponent<PlayerWeapons>().currentWeapon.GetComponent<PlayerWeapons>
            Destroy(bullet, bulletReach);
        }


        shotBullet = true;
    }

    void CheckIfShot()
    {
        if (fireRateCount <= 0)
        {
            shotBullet = false;
        }

        if (shotBullet)
        {
            fireRateCount -= Time.deltaTime;
        }
        else
        {
            fireRateCount = fireRate;
        }
        
    }
    
}

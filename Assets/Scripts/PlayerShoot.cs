using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] public float bulletSpeed;
    bool canShoot => (Input.GetMouseButton(0) && !shotBullet && GetComponent<PlayerWeapons>().weaponList.Count > 0);
    bool shotBullet = false;
    [SerializeField] public float fireRate;
    [SerializeField] float fireRateCount;
    Vector3 destination;

    void Start()
    {
        fireRateCount = fireRate;
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

        // Un solo disparo -> Pistola normal
        var bullet = Instantiate(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        // Con esta línea de abajo, haces que las balas vayan directas al centro de la pantalla, en lugar de solo ir hacia delante.
        bullet.GetComponent<Rigidbody>().velocity = (destination - bulletOrigin.position).normalized * bulletSpeed;
        Destroy(bullet, 3f);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float launchSpeed = 35f;
    public float bulletLifetime = 3f;
    public float timeBetweenShots = 6.5f;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(Vector2.left * launchSpeed, ForceMode2D.Impulse);
        Destroy(bullet, bulletLifetime);
    }
}


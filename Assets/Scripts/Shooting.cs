using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;

    public float projectileSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * projectileSpeed, ForceMode2D.Impulse);
    }
}

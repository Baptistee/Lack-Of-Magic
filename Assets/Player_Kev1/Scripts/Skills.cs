using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;

    public float projectileSpeed = 15f;
    public float shootCD = 3;
    float shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if(Input.GetButtonDown("Fire1") && shootTimer <= 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shootTimer = shootCD;
        GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * projectileSpeed, ForceMode2D.Impulse);
    }
}

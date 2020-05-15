using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 15f;
    Vector2 shootDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(Vector2 projPos, Vector2 _shootDir)
    {
        transform.position = projPos;
        shootDir = _shootDir.normalized;

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-shootDir * projectileSpeed * Time.deltaTime);
    }
}

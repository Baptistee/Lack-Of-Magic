using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles : MonoBehaviour
{

    private Transform player;
    private Vector3 target; // trajectoire ou est placé la cible

    public float speed;

    private Vector3 direction; // trajectoire du projo

    public float timeLeft; // durée de vie du projectile

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        target = new Vector2(player.position.x, player.position.y);

        // le prejectile se positionne vers le joeur
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // calcul de la trajectoire du projectile
        direction = target - transform.position;  //fixed position
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        // mouvement du projectile vers le joueur
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, direction.z * speed * Time.deltaTime, Space.World);
        
        // destruction du projectile
        timeLeft -= Time.deltaTime;
         if(timeLeft <0)
         {
             Destroy(gameObject);
         }
    }
}

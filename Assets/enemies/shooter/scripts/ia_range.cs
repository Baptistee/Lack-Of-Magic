using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ia_range : MonoBehaviour
{

    public float speed; // vitesse  de deplacement de l'ia
    public float stoppingDistance; // distance d'arret de l'ia
    public float retreatDistance; // distance de fuite 

    private float timeBetweenShots; // calcul quand lancer le tir
    public float startTimeBetweenShots; // interval du temps de tir

    public GameObject Projectile;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;

        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        //---------------------------------------Mouvement de L'ia-------------------------------------------------

        if(Vector2.Distance(transform.position,player.position) > stoppingDistance) // l'ia chasse le joueur si la distance est trop grande
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        //l'ia ne bouge pas si elle est entre la distance de fuite et d'arret
        else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) >retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance) // fuite de l'ia si le joueur est trop pret
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        //-------------------------------------Fin Mouvement de L'ia-----------------------------------------------

        //------------------------------------Tir de l'ia---------------------------------------------------------

        if(timeBetweenShots <=0) // on tire toute les tants de seconde
        {
            Instantiate(Projectile,transform.position,Quaternion.identity); // spwan du projectile a la position de l'enemi (sans rotation)
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        //----------------------------------------------------------Fin tir de L'IA-----------------------------------
    }
}

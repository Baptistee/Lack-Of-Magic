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

        //------------------------------------------------ le monstre se positionne vers le joeur--------------------------
        if (transform.position.x > player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

            //---------------------------------------Mouvement de L'ia-------------------------------------------------

        if (timeBetweenShots > 0) // on tire toute les tants de seconde ( compteur de temps);
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance) // l'ia chasse le joueur si la distance est trop grande
        {
            GetComponent<Animator>().SetBool("marche", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        //l'ia ne bouge pas si elle est entre la distance de fuite et d'arret
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            GetComponent<Animator>().SetBool("marche", false);
            

            //------------------------------------Lancement de l'animation d'attque---------------------------------------------------------
            if (timeBetweenShots <= 0) // on tire toute les tants de seconde
            {
                GetComponent<Animator>().SetBool("attaque", true);

            }
            else
            {
                GetComponent<Animator>().SetBool("attaque", false);
                timeBetweenShots -= Time.deltaTime;
            }
            //----------------------------------------------------------Fin lancement animation attaque-----------------------------------
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance) // fuite de l'ia si le joueur est trop pret
        {
            GetComponent<Animator>().SetBool("marche", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime); 
        }

        //-------------------------------------Fin Mouvement de L'ia-----------------------------------------------

        // a la fin de l'animation d'attaque on fait spawn un projectile
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attaque") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && GetComponent<Animator>().GetBool("attaque") == true) 
        {
            Vector2 pos_projo = new Vector2(transform.position.x + 1.5f, transform.position.y);
            Instantiate(Projectile, pos_projo, Quaternion.identity); // spwan du projectile a la position de l'enemi (sans rotation)
            timeBetweenShots = startTimeBetweenShots;
            transform.position = this.transform.position;
            GetComponent<Animator>().SetBool("attaque", false);
        }
    }
}

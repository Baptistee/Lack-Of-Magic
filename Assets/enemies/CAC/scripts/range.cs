using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class range : MonoBehaviour
{

    public float data_chase;
    public float range_attaque;

    public float life;
     
    private Vector3 save_collider; // sauvegarde de la position du collider

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        save_collider = transform.GetChild(0).localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // le monstre se positionne vers le joeur
        if (transform.position.x > player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.GetChild(0).localPosition = new Vector3(0.11f, 0.91f, -1.34f); // le collider de l'epee du monstre se positionnne en fonction de la pos du monstre
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.GetChild(0).localPosition = save_collider;
        }

        //Debug.Log(transform.position.x);
        //Debug.Log(transform.position.y);

        if (Vector2.Distance(transform.position, player.transform.position) < range_attaque) // on tape
        {
           // Debug.Log(Vector2.Distance(transform.position, player.transform.position));
            GetComponent<Animator>().SetBool("attaque", true);
        }
        else if (Vector2.Distance(transform.position,player.transform.position) < data_chase) // on chase
        {
            GetComponent<Animator>().SetBool("attaque", false);
            GetComponent<Animator>().SetBool("chase", true);   
        }
        else
            GetComponent<Animator>().SetBool("chase", false); // idle

        // a la fin de l'animation de prise de degat on met le boolean a faux pour qu'il repasse sur un autre etat
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("take_damadge") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            GetComponent<Animator>().SetBool("take_damadge", false);
        }
       
        if(life==0) // on lance l'animation de mort qui detruira le gameobject
        {
            GetComponent<Animator>().SetBool("take_damadge", false);
            GetComponent<Animator>().SetTrigger("dead 0");
            life--; // empeche l'animation de mort de boucler
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // collision avec un projo
    {
        if(collision.gameObject.tag=="projectileT2")
        {
            
            Destroy(collision.gameObject);
            if(GetComponent<Animator>().GetBool("attaque")==false)
                GetComponent<Animator>().SetBool("take_damadge", true);
            life --;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
    }
}

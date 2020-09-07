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

    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        save_collider = transform.GetChild(0).localPosition;
        if (GameObject.FindGameObjectsWithTag("Player")[0]!=null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
            active = true;
        }
        else
        {
            player = null;
            active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {

            //GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1,1,1,1), new Color(1, 1, 1, 0), Mathf.PingPong(Time.time, 0.3f));
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
            else if (Vector2.Distance(transform.position, player.transform.position) < data_chase) // on chase
            {
                GetComponent<Animator>().SetBool("attaque", false);
                GetComponent<Animator>().SetBool("chase", true);
            }
            else
                GetComponent<Animator>().SetBool("chase", false); // idle


            if (life == 0) // on lance l'animation de mort qui detruira le gameobject
            {
                GetComponent<Animator>().SetTrigger("dead 0");
                life--; // empeche l'animation de mort de boucler
            }
        }
    }

    //-------------------------------------------------------------------------Prise de degats --------------------------------------------------------------

    IEnumerator Blinker() // le mob clignote quand il prend des degats
    {
       // transform.GetComponent<Rigidbody2D>().constraints= RigidbodyConstraints2D.FreezeAll;

        for (int i = 0; i < 3; i++)
        {
           
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
        }

        StopCoroutine("Blinker");
    }

    IEnumerator Knockback(float knockDur, float knockbackPWR, Vector3 knockbackDir) // knockback
    {
        float timer = 0;
    
        while (knockDur > timer)
        {
            timer += Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -knockbackPWR * Time.deltaTime);
        }
        yield return 0;

    }

    private void OnCollisionEnter2D(Collision2D collision) // collision avec un projo
    {
        
        if(collision.gameObject.tag=="projectileT2")
        {
            
            Destroy(collision.gameObject);

            life --;

            if (life > 0) 
            {
                StartCoroutine(Blinker()); // clignote quand prends des degats
                StartCoroutine(Knockback(1f, 1.5f, transform.position));  // knockback quand il y a une prise de degat
            }
          
        }
    }
}

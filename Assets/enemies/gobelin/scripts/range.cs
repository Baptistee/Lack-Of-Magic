using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class range : MonoBehaviour
{

    public float data_chase;
    public float range_attaque;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        // le monstre se positionne vers le joeur
        if (transform.position.x > player.transform.position.x )
        {
            GetComponent<SpriteRenderer>().flipX=true;
        }
        else
            GetComponent<SpriteRenderer>().flipX = false;

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
    }
}

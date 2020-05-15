using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class range : MonoBehaviour
{

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,player.transform.position)<3)
        {
            GetComponent<Animator>().SetBool("chase", true);
        }
        else
            GetComponent<Animator>().SetBool("chase", false);
    }
}

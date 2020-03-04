using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 3;

    public float movespeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Vector3 dashMovement;
    public float dashDistance = 2f;
	public float dashCD = 2f;
	float dashTimer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Animation move_down
        if (movement.y < 0)
            GetComponent<Animator>().SetBool("move_down", true);
        else
            GetComponent<Animator>().SetBool("move_down", false);

        dashMovement = new Vector3(movement.x, movement.y, 0);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		
		dashTimer -= Time.deltaTime;

        //Dash
        if (Input.GetButtonDown("Jump") && dashTimer <= 0)
        {
			dashTimer = dashCD;
            transform.position += dashMovement * dashDistance;
        }

        //Mort
        if (lives <= 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //Déplacements
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);

        //Rotation vers curseur
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 180f;
        rb.rotation = angle;

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectileT1")
        {
            Destroy(collision.gameObject);
            lives--;
        }
    }
}

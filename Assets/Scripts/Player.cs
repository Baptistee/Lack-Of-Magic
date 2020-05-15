using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lives = 3;
    public Camera cam;

    //Move
    Vector2 movement;
    public float movespeed = 5f;


    //Shoot
    public Transform firePoint;
    public float shootCD = 3;
    float shootTimer = 0;
    Vector2 shootDir;
    public bool shoot_flip_x = false;
    public Projectile proj;

    //Dash
    public float dashDistance = 2f;
    public float dashCD = 2f;
    float dashTimer = 0;
    public Transform dashEffect;

    //GUI
    public Texture2D shootTexture;
    public Texture2D shootTextureCD;
    public Texture2D dashTexture;
    public Texture2D dashTextureCD;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //déplacements
        transform.Translate(movement * movespeed * Time.deltaTime);

        //Animations
        //move_down
        if (movement.y < 0)
            GetComponent<Animator>().SetBool("move_down", true);
        else
            GetComponent<Animator>().SetBool("move_down", false);

        //move_up
        if (movement.y > 0)
            GetComponent<Animator>().SetBool("move_up", true);
        else
            GetComponent<Animator>().SetBool("move_up", false);

        //move_left && move_right
        if (movement.x != 0)
        {
            GetComponent<Animator>().SetBool("move_down", false);
            GetComponent<Animator>().SetBool("move_up", false);

            if (movement.x < 0)
                GetComponent<SpriteRenderer>().flipX = false;
            else
                GetComponent<SpriteRenderer>().flipX = true;

            GetComponent<Animator>().SetBool("move_left", true);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("move_left", false);
        }

        //Shoot
        shootTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && shootTimer <= 0)
        {
            ShootAnim();
        }

        //Dash
        dashTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && dashTimer <= 0)
        {
            Dash();
        }

        //Mort
        if (lives <= 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //Déplacements
        

        //Rotation vers curseur
        //Vector2 lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 180f;
        //rb.rotation = angle;
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "projectileT1")
        {
            Destroy(collision.gameObject);
            lives--;
        }
    }

    void ShootAnim()
    {
        shootTimer = shootCD;

        //Animation
        Vector3 lookDir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        shootDir = new Vector2(lookDir.x, lookDir.y);
        Debug.Log(shootDir);
        if(shootDir.x < -0.5f)
        {
            Debug.Log("Tir vers la gauche");
            GetComponent<Animator>().SetTrigger("shoot_left");
        }
        else if(shootDir.x > 0.5f)
        {
            Debug.Log("Tir vers la droite");
            shoot_flip_x = true;
            GetComponent<Animator>().SetTrigger("shoot_left");
        }
        else if(shootDir.y > 0)
        {
            Debug.Log("Tir vers le haut");
            GetComponent<Animator>().SetTrigger("shoot_left");
        }
        else
        {
            Debug.Log("Tir vers le bas");
            GetComponent<Animator>().SetTrigger("shoot_left");
        }

        
        //Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.right * projectileSpeed, ForceMode2D.Impulse);
    }

    public void Shoot()
    {
        Projectile pr = Instantiate(proj) as Projectile;
        pr.init(transform.position, shootDir);
    }

    public void Dash()
    {
        dashTimer = dashCD;

        //Pour calculer l'angle entre deux points
        Vector3 beforeDashPosition = transform.position;
        Vector3 dashMovement = new Vector3(movement.x, movement.y, 0);
        Vector3 afterDashPosition = transform.position + dashMovement * dashDistance;
        Vector2 dif = afterDashPosition - beforeDashPosition;
        float sign = (afterDashPosition.y < beforeDashPosition.y) ? -1f : 1f;

        //Animation
        float dashEffectWidth = 1f;
        Transform dashEffectTransform = Instantiate(dashEffect, beforeDashPosition, Quaternion.identity);
        dashEffectTransform.eulerAngles = new Vector3(0, 0, Vector2.Angle(Vector2.right, dif) * sign);
        dashEffectTransform.localScale = new Vector3(dashDistance / dashEffectWidth, 1f, 1f);

        //Déplacement dash
        transform.position += dashMovement * dashDistance;
    }

    private void OnGUI()
    {
        //Shoot UI
        if(shootTimer <= 0)
        {
            GUI.Label(new Rect(10, 10, 100, 100), shootTexture);
        }
        else
        {
            GUI.Label(new Rect(10, 10, 100, 100), shootTextureCD);
        }

        //Dash UI
        if (dashTimer <= 0)
        {
            GUI.Label(new Rect(120, 10, 100, 100), dashTexture);
        }
        else
        {
            GUI.Label(new Rect(120, 10, 100, 100), dashTextureCD);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Vector3 dashMovement;
    public float dashDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        dashMovement = new Vector3(movement.x, movement.y, 0);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Dash
        if (Input.GetButtonDown("Jump"))
        {
            transform.position += dashMovement * dashDistance;
        }
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
}

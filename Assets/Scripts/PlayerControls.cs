using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode rotateLeft;
    public KeyCode rotateRight;

    public float speed = 10f;

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        movePlayer();
        rotatePlayer();
        shoot();
    }

    private void shoot()
    {
        //TODO: shoot projectile
    }

    private void shipToOppositeWall(Transform wallTr2D)
    {
        if(wallTr2D.name == "topWall" && rb2D.transform.position.y > wallTr2D.position.y)
        {
            rb2D.position = new Vector2(rb2D.position.x, -rb2D.position.y);
        }
        else if (wallTr2D.name == "bottomWall" && rb2D.transform.position.y < wallTr2D.position.y)
        {
            rb2D.position = new Vector2(rb2D.position.x, -rb2D.position.y);
        }
        else if (wallTr2D.name == "rightWall" && rb2D.transform.position.x > wallTr2D.position.x)
        {
            rb2D.position = new Vector2(-rb2D.position.x, rb2D.position.y);
        }
        else if (wallTr2D.name == "leftWall" && rb2D.transform.position.x < wallTr2D.position.x)
        {
            rb2D.position = new Vector2(-rb2D.position.x, rb2D.position.y);
        }
    }

    void rotatePlayer()
    {
        if (Input.GetKey(rotateLeft))
        {
            rb2D.rotation += speed;
        }
        else if(Input.GetKey(rotateRight))
        {
            rb2D.rotation -= speed;
        }
    }
    void movePlayer()
    {
        //move up and right
        if (Input.GetKey(moveUp) && Input.GetKey(moveRight))
        {
            rb2D.AddForce(new Vector2(speed, speed));
        }
        //move up and left
        else if (Input.GetKey(moveUp) && Input.GetKey(moveLeft))
        {
            rb2D.AddForce(new Vector2(-speed, speed));
        }
        //move down and right
        else if (Input.GetKey(moveDown) && Input.GetKey(moveRight))
        {
            rb2D.AddForce(new Vector2(speed, -speed));
        }
        //move down and left
        else if (Input.GetKey(moveDown) && Input.GetKey(moveLeft))
        {
            rb2D.AddForce(new Vector2(-speed, -speed));
        }
        //move up
        else if (Input.GetKey(moveUp))
        {
            rb2D.AddForce(new Vector2(0, speed));
        }
        //move down
        else if (Input.GetKey(moveDown))
        {
            rb2D.AddForce(new Vector2(0, -speed));
        }
        //move right
        else if (Input.GetKey(moveRight))
        {
            rb2D.AddForce(new Vector2(speed, 0));
        }
        //move left
        else if (Input.GetKey(moveLeft))
        {
            rb2D.AddForce(new Vector2(-speed, 0));
        }
        else
        {
        }
    }
}

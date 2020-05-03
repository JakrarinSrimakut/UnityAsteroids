using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public float speed = 10f;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update ()
    {
        /*TODO: 1.MoveShip: A,W,S,D
         *      2.Rotate ship: Mouse
         * 
         * */
        if (Input.GetKey(moveUp) && Input.GetKey(moveRight))
        {
            rb2D.velocity = new Vector2(speed, speed);
        }
        else if (Input.GetKey(moveUp) && Input.GetKey(moveLeft))
        {
            rb2D.velocity = new Vector2(-speed, speed);
        }
        else if (Input.GetKey(moveDown) && Input.GetKey(moveRight))
        {
            rb2D.velocity = new Vector2(speed, -speed);
        }
        else if (Input.GetKey(moveDown) && Input.GetKey(moveLeft))
        {
            rb2D.velocity = new Vector2(-speed, -speed);
        }
        //move up
        else if (Input.GetKey(moveUp))
        {
            rb2D.velocity = new Vector2(0, speed);
        }
        //move down
        else if (Input.GetKey(moveDown))
        {
            rb2D.velocity = new Vector2(0, -speed);
        }
        //move right
        else if (Input.GetKey(moveRight))
        {
            rb2D.velocity = new Vector2(speed, 0);
        }
        //move left
        else if (Input.GetKey(moveLeft))
        {
            rb2D.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb2D.velocity = Vector2.zero;
        }
    }
}

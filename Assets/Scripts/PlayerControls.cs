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
    public KeyCode shoot;

    public float projectileForce;
    public GameObject projectile;
    public float speed = 10f;
    public float shipOffsetY;
    public float shipOffsetX;

    float screenDepth;
    Vector3 screenLowerLeftCorner;
    Vector3 screenUpperRightCorner;
    float screenMinX;
    float screenMaxX;
    float screenMinY;
    float screenMaxY;

    private Rigidbody2D rb2D;
    private Transform tr2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        tr2D = GetComponent<Transform>();
        screenDepth = -Camera.main.transform.position.z;
        screenLowerLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, screenDepth));
        screenUpperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, screenDepth));
        screenMinX = screenLowerLeftCorner.x;
        screenMaxX = screenUpperRightCorner.x;
        screenMinY = screenLowerLeftCorner.y;
        screenMaxY = screenUpperRightCorner.y;
    }
    
    // Update is called once per frame
    void Update ()
    {
        movePlayer();
        rotatePlayer();
        shootProjectile();
        shipToOppositeWall();
    }

    private void shootProjectile()
    {
        if (Input.GetButtonDown("Fire1")){
            GameObject projectileBeam = Instantiate(projectile, transform.position, transform.rotation);
            projectileBeam.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * projectileForce);
            Destroy(projectileBeam, 5.0f);
        }
    }

    private void shipToOppositeWall()
    {
        if (tr2D.position.y - shipOffsetY > screenMaxY)
        {
            tr2D.position = new Vector2(tr2D.position.x, screenMinY);
        }
        if (tr2D.position.y + shipOffsetY < screenMinY)
        {
            tr2D.position = new Vector2(tr2D.position.x, screenMaxY);
        }
        if (tr2D.position.x - shipOffsetX > screenMaxX)
        {
            tr2D.position = new Vector2(screenMinX, tr2D.position.y);
        }
        if (tr2D.position.x + shipOffsetX < screenMinX)
        {
            tr2D.position = new Vector2(screenMaxX, tr2D.position.y);
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

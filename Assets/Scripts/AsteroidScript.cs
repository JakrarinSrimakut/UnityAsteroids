using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidScript : MonoBehaviour {

    public float maxThrust;
    public float maxTorque;
    public float asteroidOffsetY;
    public float asteroidOffsetX;
    public GameObject subAsteroid;
    public int asteroidPoint;
    public GameObject player;
    public GameObject explosion;
    public GameManager gm;

    private Rigidbody2D rb2D;
    private Transform tr2D;

    float screenDepth;
    Vector3 screenLowerLeftCorner;
    Vector3 screenUpperRightCorner;
    float screenMinX;
    float screenMaxX;
    float screenMinY;
    float screenMaxY;

    // Use this for initialization
    void Start () {
        //add ranodm thrust and torque to asteroid
        rb2D = GetComponent<Rigidbody2D>();
        tr2D = GetComponent<Transform>();
        screenDepth = -Camera.main.transform.position.z;
        screenLowerLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, screenDepth));
        screenUpperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, screenDepth));
        screenMinX = screenLowerLeftCorner.x;
        screenMaxX = screenUpperRightCorner.x;
        screenMinY = screenLowerLeftCorner.y;
        screenMaxY = screenUpperRightCorner.y;

        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb2D.AddForce(thrust);
        rb2D.AddTorque(torque);

        //find player
        player = GameObject.FindWithTag("Player");
        //find the Game Manager
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update () {
        asteroidToOppositeWall();
	}

    // Asteroid collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check to see if it's a projectile
        if (collision.CompareTag("projectile"))
        {
            //Debug.Log("projectile collided");
            //destory projectile
            Destroy(collision.gameObject);
            //Make subAsteroid if not small asteroid
            if(subAsteroid != null)
            {
                //spawn 2 medium asteroids
                Instantiate(subAsteroid, transform.position, transform.rotation);
                Instantiate(subAsteroid, transform.position, transform.rotation);
                gm.UpdateNumberOfAsteroids(1);
            }
            else
            {
                gm.UpdateNumberOfAsteroids(-1);
            }

            //Tell player to update score
            player.SendMessage("scorePoint", asteroidPoint);

            //Make Explostion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);

            //remove current asteroid
            Destroy(gameObject);
        }
    }

    //Move asteroid to opposite wall when pass wall's boundery
    private void asteroidToOppositeWall()
    {
        if (tr2D.position.y - asteroidOffsetY > screenMaxY)
        {
            tr2D.position = new Vector2(tr2D.position.x, screenMinY);
        }
        if (tr2D.position.y + asteroidOffsetY < screenMinY)
        {
            tr2D.position = new Vector2(tr2D.position.x, screenMaxY);
        }
        if (tr2D.position.x - asteroidOffsetX > screenMaxX)
        {
            tr2D.position = new Vector2(screenMinX, tr2D.position.y);
        }
        if (tr2D.position.x + asteroidOffsetX < screenMinX)
        {
            tr2D.position = new Vector2(screenMaxX, tr2D.position.y);
        }
    }
}

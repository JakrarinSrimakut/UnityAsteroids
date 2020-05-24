using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour {

    public Rigidbody2D rb;
    public Vector2 direction;
    public float speed;
    public float shootingDelay; //time between shots in seconds
    public float lastTimeShot = 0f;
    public float bulletSpeed;
    public Transform player;
    public GameObject bullet;
    public GameObject explosion;
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public bool isDisabled;   //true when currently disabled
    public int points;
    public float timeBeforeSpawning;
    public float levelStartTime;
    public Transform startPosition;


	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag ("Player").transform;
        levelStartTime = Time.time;
        timeBeforeSpawning = Random.Range(5f, 10f);
        Invoke("enable", timeBeforeSpawning);
        disable();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDisabled)
        {
            return;
        }
		if(Time.time > lastTimeShot + shootingDelay)
        {
            //Shoot
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; //Atan2 returns radians multiply Rad2Deg into degrees - 90 degrees
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            //Make a bullet
            GameObject newBullet = Instantiate(bullet, transform.position, q); //q points bullet at player
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
            lastTimeShot = Time.time;
        }
	}

    private void FixedUpdate()
    {
        if (isDisabled)
        {
            return;
        }
        //Figure out which way to move to approach the player
        direction = (player.position - transform.position).normalized;  //normalized to 1 so alien don't go faster if distance is further
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void enable()
    {
        //Move to start position
        transform.position = startPosition.position;
        //Turn on collider and sprite
        collider.enabled = true;
        spriteRenderer.enabled = true;
        isDisabled = false;

    }

    private void disable()
    {
        //turn of collider and sprite renderer
        collider.enabled = false;
        spriteRenderer.enabled = false;
        isDisabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("projectile"))
        {
            //tell player to score points
            player.SendMessage("scorePoint", points);

            //Destroy the alien
            //Explosion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            disable();
        }
    }
}

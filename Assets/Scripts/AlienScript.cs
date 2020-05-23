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
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
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
        //Figure out which way to move to approach the player
        direction = (player.position - transform.position).normalized;  //normalized to 1 so alien don't go faster if distance is further
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}

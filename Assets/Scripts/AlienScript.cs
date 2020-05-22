using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour {

    public Rigidbody2D rb;
    public Vector2 direction;
    public float speed;

    public Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //Figure out which way to move to approach the player
        direction = (player.position - transform.position).normalized;  //normalized to 1 so alien don't go faster if distance is further
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}

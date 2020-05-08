using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

    public float maxThrust;
    public float maxTorque;
    private Rigidbody2D rb2D;

    //offset for asteroid to fully pass wall before teleporting it to opposite wall
    public float screenTopOffset;
    public float screenBottomOffset;
    public float screenLeftOffset;
    public float screenRightOffset;

    // Use this for initialization
    void Start () {
        //add ranodm thrust and torque to asteroid
        rb2D = GetComponent<Rigidbody2D>();

        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb2D.AddForce(thrust);
        rb2D.AddTorque(torque);
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    //TODO: Find offset of asteroid to walls
    private void asteroidToOppositeWall(Transform wallTr2D)
    {
        if (wallTr2D.name == "topWall" && rb2D.transform.position.y > wallTr2D.position.y)
        {
            rb2D.position = new Vector2(rb2D.position.x, -rb2D.position.y);
        }
        if (wallTr2D.name == "bottomWall" && rb2D.transform.position.y < wallTr2D.position.y)
        {
            rb2D.position = new Vector2(rb2D.position.x, -rb2D.position.y);
        }
        if (wallTr2D.name == "rightWall" && rb2D.transform.position.x > wallTr2D.position.x)
        {
            rb2D.position = new Vector2(-rb2D.position.x, rb2D.position.y);
        }
        if (wallTr2D.name == "leftWall" && rb2D.transform.position.x < wallTr2D.position.x)
        {
            rb2D.position = new Vector2(-rb2D.position.x, rb2D.position.y);
        }
    }
}

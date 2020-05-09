using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

    public float maxThrust;
    public float maxTorque;

    public float asteroidOffsetY;
    public float asteroidOffsetX;

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
        
	}
	
	// Update is called once per frame
	void Update () {
        asteroidToOppositeWall();
	}

    //TODO: Find offset of asteroid to walls
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

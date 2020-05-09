using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

    public Camera mainCam;
    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;

    // Use this for initialization
    void Start () {
        //topWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * 2f, 1f);
        //topWall.offset = new Vector2(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);

        //bottomWall.size = topWall.size;
        //bottomWall.offset = -topWall.offset;

        //leftWall.size = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y * 2f);
        //leftWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, 0f);

        //rightWall.size = leftWall.size;
        //rightWall.offset = -leftWall.offset;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

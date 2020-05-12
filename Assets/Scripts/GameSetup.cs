using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour {

    public Text scoreText;
    public Text livesText;
    private int lives = 3;
    private int score = 0;

    // Use this for initialization
    void Start () {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //score increment
    public void scorePoint(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
    }

    //life decrement
    public void lossLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int numberOfAsteroids; //This is the current number of asteroids in the scene
    public int levelNumber = 1;
    public GameObject asteroid;

    float screenDepth;
    Vector3 screenLowerLeftCorner;
    Vector3 screenUpperRightCorner;
    float screenMinX;
    float screenMaxX;
    float screenMinY;
    float screenMaxY;

    void Start()
    {
        //add ranodm thrust and torque to asteroid
        screenDepth = -Camera.main.transform.position.z;
        screenLowerLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, screenDepth));
        screenUpperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, screenDepth));
        screenMinX = screenLowerLeftCorner.x;
        screenMaxX = screenUpperRightCorner.x;
        screenMinY = screenLowerLeftCorner.y;
        screenMaxY = screenUpperRightCorner.y;
    }
    public void UpdateNumberOfAsteroids(int change)
    {
        numberOfAsteroids += change;

        //Check to see if we have any asteroids left
        if(numberOfAsteroids <= 0)
        {
            //Start new level
            Invoke("StartNewLevel", 3f);
        }

    }

    void StartNewLevel()
    {
        levelNumber++;

        //Spawn New Asteroids
        for (int i = 0; i < levelNumber*2; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(screenMinX, screenMaxX), screenMaxY);
            Instantiate(asteroid, spawnPosition, Quaternion.identity);
            numberOfAsteroids++;
        }
    }


}

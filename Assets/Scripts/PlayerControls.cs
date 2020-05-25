using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float thrust = 10f;
    public float turnThrust = 10f;
    public float shipOffsetY;
    public float shipOffsetX;
    public float deathForce;
    public Text livesText;
    public Text scoreText;
    public AudioSource audio;
    public GameObject explosion;
    public Color inColor;
    public Color normalColor;
    public GameObject gameOverPanel;
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public AlienScript alien;
    public int currentLevel = 0;

    private float thrustInput;
    private float turnInput;
    public int lives;
    private int score = 0;
    private bool isHyperspace;  //true - currently hyperspacing

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
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        isHyperspace = false;

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
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        rotatePlayer();
        shootProjectile();
        shipToOppositeWall();
        if (Input.GetButtonDown("Hyperspace") && !isHyperspace)
        {
            isHyperspace = true;
            //Turn off colliders and spriteRenderer
            spriteRenderer.enabled = false;
            collider.enabled = false;
            Invoke("hyperspace", 1f);

        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    void hyperspace()
    {
        //Move to a new random position
        Vector2 newPosition = new Vector2(Random.Range(screenMinX + shipOffsetX, screenMaxX - shipOffsetX), Random.Range(screenMinY + shipOffsetY, screenMaxY - shipOffsetY));
        transform.position = newPosition;
        //Turn on colliders and spriteRenderer
        spriteRenderer.enabled = true;
        collider.enabled = true;

        isHyperspace = false;
    }

    void respawn()
    {
        rb2D.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        spriteRenderer.enabled = true;
        spriteRenderer.color = inColor;
        Invoke("invulnerable", 3f);
    }

    void invulnerable()
    {
        collider.enabled = true;
        spriteRenderer.color = normalColor;
    }

    //life decrement
    public void lossLife()
    {
        lives--;
        //Make Explosion
        GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(newExplosion, 3f);

        livesText.text = "Lives: " + lives;

        spriteRenderer.enabled = false;
        collider.enabled = false;
        Invoke("respawn", 3f);
        if (lives <= 0)
        {
            //GameOver
            gameOver();
        }
    }

    //player collision 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision mag: " + collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude > deathForce)
        {
            lossLife();
        }
        else
        { 
            audio.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("alienbeam"))
        {
            lossLife();
            alien.disable(); //disable alien until next level so it won't barrage player when respawning
        }
    }

    //score increment
    public void scorePoint(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
        Debug.Log("score: " + score);
    }

    void gameOver()
    {
        CancelInvoke();
        gameOverPanel.SetActive(true);
    }

    public void playAgain()
    {
        SceneManager.LoadScene("MainLevel");
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

    //rotate ship
    void rotatePlayer()
    {
        transform.Rotate(Vector3.forward * -turnInput * Time.deltaTime *turnThrust);
        //if (Input.GetKey(rotateLeft))
        //{
        //    //rb2D.AddTorque(turnThrust);
        //    tr2D.Rotate(Vector3.forward * Time.deltaTime * turnThrust);
        //}
        //else if(Input.GetKey(rotateRight))
        //{
        //    //rb2D.AddTorque(-turnThrust);
        //    tr2D.Rotate(-Vector3.forward * Time.deltaTime * turnThrust);
        //}
    }

    //move ship
    void movePlayer()
    {
        ////move up and right
        //if (Input.GetKey(moveUp) && Input.GetKey(moveRight))
        //{
        //    rb2D.AddForce(new Vector2(thrust, thrust));
        //}
        ////move up and left
        //else if (Input.GetKey(moveUp) && Input.GetKey(moveLeft))
        //{
        //    rb2D.AddForce(new Vector2(-thrust, thrust));
        //}
        ////move down and right
        //else if (Input.GetKey(moveDown) && Input.GetKey(moveRight))
        //{
        //    rb2D.AddForce(new Vector2(thrust, -thrust));
        //}
        ////move down and left
        //else if (Input.GetKey(moveDown) && Input.GetKey(moveLeft))
        //{
        //    rb2D.AddForce(new Vector2(-thrust, -thrust));
        //}
        //move up
        if (Input.GetKey(moveUp))
        {
            rb2D.AddRelativeForce(Vector2.up * thrustInput * thrust);
        }
        //move down
        //else if (Input.GetKey(moveDown))
        //{
        //    rb2D.AddForce(new Vector2(0, -thrust));
        //}
        ////move right
        //else if (Input.GetKey(moveRight))
        //{
        //    rb2D.AddForce(new Vector2(thrust, 0));
        //}
        ////move left
        //else if (Input.GetKey(moveLeft))
        //{
        //    rb2D.AddForce(new Vector2(-thrust, 0));
        //}
        //else
        //{
        //}
    }
}

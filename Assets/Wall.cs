using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private Transform tr2D;

    private void Start()
    {
        tr2D = GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: send ship to opposite wall
        Debug.Log("Old Position " + collision.GetComponent<Rigidbody2D>().transform.position.x + "," + collision.GetComponent<Rigidbody2D>().transform.position.x);
        collision.gameObject.SendMessage("shipToOppositeWall", tr2D);
        Debug.Log("New Position " + collision.GetComponent<Rigidbody2D>().transform.position.x + "," + collision.GetComponent<Rigidbody2D>().transform.position.x);

    }
}

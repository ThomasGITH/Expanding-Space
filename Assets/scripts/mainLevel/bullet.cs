using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float bulletSpeed = 200;
    public GameObject player;
    private float moveX, moveY;
    public int direction;

    // Use this for initialization
    void Start () {
        if(direction == 2)
        {
            gameObject.transform.Rotate(Vector3.forward * -90);
        }

        GameObject[] player_list = GameObject.FindGameObjectsWithTag("Player");
        player = player_list[0];
    }
	
	// Update is called once per frame
	void Update () {

        //DETERMINE DIRECTION
        if (direction == 0) {
            moveX = -1;
            moveY = GetComponent<Rigidbody2D>().velocity.y;
        }
        else if(direction == 1){
            moveX = 1;
            moveY = GetComponent<Rigidbody2D>().velocity.y;
        }
        else
        {
            moveX = GetComponent<Rigidbody2D>().velocity.x;
            moveY = 1 * bulletSpeed;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * bulletSpeed, moveY);

        killWhenOutOfRange();
	}

    //VERANDER DIT LATER NOG
    void killWhenOutOfRange()
    {
        if ((transform.position.x < -50) || (transform.position.x > 50) || (transform.position.y < -50) || (transform.position.y > 50))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            print("DETECTING PLATFORM");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "platform")|| (collision.gameObject.tag == "enemy"))
        {
            Destroy(gameObject);
        }
    }
}

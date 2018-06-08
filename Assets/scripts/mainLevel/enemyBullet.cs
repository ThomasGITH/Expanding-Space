﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{

    public float bulletSpeed = 5;
    private float moveX, moveY;
    public int direction;

    // Use this for initialization
    void Start()
    {
        if (direction == 2)
        {
            gameObject.transform.Rotate(Vector3.forward * -90);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //DETERMINE DIRECTION
        if (direction == 0)
        {
            moveX = -1;
            moveY = GetComponent<Rigidbody2D>().velocity.y;
        }
        else if (direction == 1)
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((collision.tag == "platform")||(collision.tag == "Player"))||(collision.tag == "base"))
        {
            Destroy(gameObject);
        }
    }
}

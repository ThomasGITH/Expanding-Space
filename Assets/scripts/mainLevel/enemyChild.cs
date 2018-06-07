using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChild : MonoBehaviour {

    public bool isGrounded;
	
	// Update is called once per frame
	void Update () {

	}
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "ground") || (collision.gameObject.tag == "platform") || (collision.gameObject.tag == "base") || (collision.gameObject.tag == "powerUp"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
            //if (!(collision.gameObject.tag == "ground") || !(collision.gameObject.tag == "platform") || !(collision.gameObject.tag == "base"))
            //{
            isGrounded = false;
            //}
    }
}

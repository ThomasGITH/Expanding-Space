using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPackage : MonoBehaviour {

    public GameObject boss;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResizeCollider()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.01332211f, -0.05327022f);
        GetComponent<BoxCollider2D>().size = new Vector2(1.089659f, 1.016474f);
    }

    public void ResizeColliderAgain()
    {
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.2139589f, -0.1137573f);
        GetComponent<BoxCollider2D>().size = new Vector2(1.490932f, 1.72394f);
    }

    public void Activate()
    {
        var largeEnemy = (GameObject)Instantiate(boss, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        Destroy(gameObject);
    }

}

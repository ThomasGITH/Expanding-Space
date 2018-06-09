using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class enemySpawn : MonoBehaviour {

    public GameObject dropShip, warning, textObject, cameraObj, popup;
    GameObject enemyDropShip, watchOut, camera;
    Text waveCounter;
    //public GameObject[] enemies;
    private int timer = 300, number_of_wave = 0, enemiesLeft;
    private bool takenOff = false, hasExecuted = false, disablePopup;

	// Use this for initialization
	void Start () {
        waveCounter = textObject.GetComponent<Text>();
	}

    // Update is called once per frame
    void Update() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemiesLeft = enemies.Length;

        if ((enemiesLeft <= 0) && (!takenOff))
        {
            timer -= 1;
            if (timer <= 0) {
                number_of_wave++;
                enemyDropShip = (GameObject)Instantiate(dropShip, new Vector2(transform.position.x, transform.position.y), transform.rotation);
                enemyDropShip.GetComponent<dropship>().waveNumber = number_of_wave;
                takenOff = true;
                timer = 300;
                hasExecuted = false;
            }
            else {
                if(number_of_wave != 0)
                {
                    if ((!hasExecuted)&&(!disablePopup))
                    {
                        popup.SetActive(true);
                        popup.GetComponent<popup>().waveNumber = number_of_wave - 1;
                        hasExecuted = true;
                    }
                }
            }
        }

        if (enemyDropShip == null) { takenOff = false; }

        if (timer <= 150)
        {
            warning.SetActive(true);
        }
        else { warning.SetActive(false); }

        int wavePortrayal = number_of_wave + 1;
        if (timer <= 150)
        {
            waveCounter.enabled = true;
            waveCounter.text = "Wave " + wavePortrayal;
        }
        else { waveCounter.enabled = false; }

        if((Input.GetKeyDown(KeyCode.M))&&(popup.active == true))
        {
            disablePopup = true;
        }

        //textObject.GetComponent<Rigidbody2D>().velocity = new Vector2(cameraObj.GetComponent<Rigidbody2D>().velocity.x, cameraObj.GetComponent<Rigidbody2D>().velocity.y);
        //textObject.transform.Translate(new Vector2(cameraObj.transform.position.x, cameraObj.transform.position.y));
    }
}
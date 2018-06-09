using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour {

    public Sprite pop1, pop2, pop3, pop4, pop5, pop6, pop7, pop8, pop9, pop10, pop11, pop12;
    public int waveNumber;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        Sprite[] fields = { pop1, pop2, pop3, pop4, pop5, pop6, pop7, pop8, pop9, pop10, pop11, pop12 };

        GetComponent<SpriteRenderer>().sprite = fields[waveNumber];

        print(GetComponent<SpriteRenderer>().sprite);

        if ((Input.GetKeyDown(KeyCode.Escape))||(Input.GetKeyDown(KeyCode.M)))
        {
            gameObject.SetActive(false);
        }

    }
}

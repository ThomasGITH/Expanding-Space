using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEasterEgg : MonoBehaviour {

    public GameObject FidgetSpinner;
    public float SpinSpeed = 1;
    private bool SpinOrNot = false;

    void Start()
    {
        SpinOrNot = false;
    }

    void Update()
    {
        if (SpinOrNot)
        {
            transform.Rotate(0, 0, SpinSpeed * Time.deltaTime);
        }
    }

    public void StartSpinning()
    {
        SpinOrNot = true;
        if (SpinOrNot)
        {
            SpinSpeed = SpinSpeed + 10;
        }
    }
}

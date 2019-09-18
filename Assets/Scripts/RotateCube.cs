using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    // Set the speed of rotation
    public float vel = 100f;

    void Start()
    {


    }

    void Update()
    {
        //Rotate the frame
        transform.Rotate(Vector3.right, vel * Time.deltaTime);
    }
}

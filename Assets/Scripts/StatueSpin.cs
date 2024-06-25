using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class StatueSpin : MonoBehaviour
{    
    public float rotationSpeed = 100f; // Adjust the rotation speed as needed

    // Update is called once per frame
    void Update()
    {
        // Rotate the coin around its up axis
        transform.Rotate(UnityEngine.Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

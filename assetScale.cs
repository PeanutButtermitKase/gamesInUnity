using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assetScale : MonoBehaviour
{
    public float newSizeX = 500.0f;
    public float newSizeY = 500.0f;
    public float newSizeZ = 500.0f;
    
    void Start()
    {
        // Get the Transform component of the object
        Transform objectTransform = transform;
        // Change the size using localScale
        objectTransform.localScale = new Vector3(newSizeX, newSizeY, newSizeZ);
    }
    void Update()
    {
    
    }
}

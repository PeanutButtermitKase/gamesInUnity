using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assetAction : MonoBehaviour

{
    public float moveSpeed = 5f; //  movement speed

    void Update()
    {
        // get controller asset
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput   = Input.GetAxis("Vertical");   

        // set axis Horizontal,0,Vertical
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput); 
        // Move asset
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}

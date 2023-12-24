using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionRock : MonoBehaviour
{
// OnCollisionEnter is called when a collision is detected
    public GameObject assetTarget;
    
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves a specific tag or layer
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision with obstacle detected!");
            for (var i = 40; i < 50; i+=2)
            {
                var duplicateAsset = GameObject.Instantiate(assetTarget);
                duplicateAsset.transform.position = new Vector3(0,0,10+i);
            }
            // Perform actions or set flags when a collision occurs
        }
    }
}

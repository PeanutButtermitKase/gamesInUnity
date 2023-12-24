using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMaker : MonoBehaviour
{
    public GameObject sphere;
    void Start()
    {
        for (var i = 0; i < 4; i++)
        {
            var duplicatesphere = GameObject.Instantiate(sphere);
            duplicatesphere.transform.position = new Vector3(i,0,0);
        }
    }
    void Update()
    {
        
    }
}

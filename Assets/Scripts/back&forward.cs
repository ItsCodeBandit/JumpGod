using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pingpong : MonoBehaviour
{
    float x1, x2;
    private float lastXPosition;

    void Start() 
    {  
        x1 = transform.position.x;
        x2 = transform.position.x + 5;
        lastXPosition = transform.position.x;
    }

    void Update() 
    { 
        
        Vector3 newPosition = new Vector3(
            Mathf.PingPong(Time.time * 2, x2 - x1) + x1, 
            transform.position.y, 
            transform.position.z
        );

        
        if (newPosition.x > lastXPosition)
        {
            
            transform.rotation = Quaternion.Euler(0, 0, 0); 
        }
        else if (newPosition.x < lastXPosition)
        {
            
            transform.rotation = Quaternion.Euler(0, 180, 0); 
        }

        
        transform.position = newPosition;

        
        lastXPosition = newPosition.x;
    }
}
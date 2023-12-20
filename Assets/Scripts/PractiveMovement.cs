using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PractiveMovement : MonoBehaviour
{
    public bool move, Rotate;

    public float movementSpeed, rotationSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position += new Vector3(movementSpeed, 0f, 0f) * Time.deltaTime;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = Square.transform.position + new Vector3(0,0,-10);
    }
}

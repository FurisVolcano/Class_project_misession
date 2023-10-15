using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0,0,-10);
    }
}

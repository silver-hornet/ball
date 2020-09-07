using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopDown : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0, 15, 6);
    [SerializeField] GameObject player;
    
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
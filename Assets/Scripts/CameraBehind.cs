using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehind : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0, 3, -4);
    [SerializeField] GameObject player;

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
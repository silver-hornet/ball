using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemySimple : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }
}
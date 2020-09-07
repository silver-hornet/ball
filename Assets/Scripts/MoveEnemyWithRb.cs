using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyWithRb : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody enemyRb;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    void Update() //TODO Should this be fixedupdate instead?
    {
        enemyRb.AddForce(Vector3.back * speed);
    }
}
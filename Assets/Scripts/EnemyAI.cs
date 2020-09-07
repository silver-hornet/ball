using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    Rigidbody enemyRb;
    GameManager gameManager;
    GameObject player;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player Sphere (Physics)");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update() //TODO Should this be fixedupdate instead?
    {
        if (gameManager.isGameActive == true)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            //Using .normalized ensures the magnitude remains constant so the enemy will always be coming at the player at the same speed

            enemyRb.AddForce(lookDirection * speed);
        }
    }
}
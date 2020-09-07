using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    float belowGround = -1;
    float aboveGround = -25;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        BelowGround();
        AboveGround();
        ProjectileOutOfBounds();
    }

    void BelowGround()
    //Use this for an enemy with physics that will naturally fall off the edge of the ground
    {
        if (transform.position.y < belowGround)
        {
            Destroy(gameObject);
            gameManager.gameOver();
        }
    }

    void AboveGround()
    //Use this for an enemy without physics, since otherwise the enemy will continue above ground into infinity
    {
        if (transform.position.z < aboveGround)
        {
            Destroy(gameObject);
            gameManager.gameOver();
        }
    }

    void ProjectileOutOfBounds()
    //This is just being used to destroy the projectile the player fires
    {
        if (transform.position.z > -aboveGround)
        {
            Destroy(gameObject);
        }
    }
}
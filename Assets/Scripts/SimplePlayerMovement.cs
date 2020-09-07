//This script is no longer as up-to-date as PhysicsPlayerMovement.cs
//TODO Decide what to do with this script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    bool isOnGround = true;
    float boundaryRange = 20;
    [SerializeField] GameObject playerProjectilePrefab;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip powerupSound;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        SimpleMovement(); //I like to split these out into my own functions to keep code clean and readable
        Jump();
        PlayerBoundaries();
        FireProjectile();
    }

    void SimpleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    void Jump()
    //This jump motion is not as smooth as I'd like
    //If player does not have a rigidbody attached, this method will only move the player up the z axix by jumpForce value
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime * speed);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            isOnGround = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(collisionSound, 1.0f);
            Debug.Log("Player has collided with enemy.");
        }
    }

    void OnTriggerEnter(Collider other)
    //This doesn't work if the powerup does not have a rigidbody attached
    //Will have to find another way of doing this
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            Destroy(other.gameObject);
            Debug.Log("Player picked up powerup.");
        }

    }

    void PlayerBoundaries()
    {
        if (transform.position.x < -boundaryRange)
        {
            transform.position = new Vector3(-boundaryRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > boundaryRange)
        {
            transform.position = new Vector3(boundaryRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -boundaryRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundaryRange);
        }

        if (transform.position.z > boundaryRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryRange);
        }
    }

    void FireProjectile()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(playerProjectilePrefab, transform.position, playerProjectilePrefab.transform.rotation);
            playerAudio.PlayOneShot(shootSound, 1.0f);
        }
    }
}
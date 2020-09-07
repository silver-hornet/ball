using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityModifier; //This is optional to modify the gravity
    [SerializeField] bool hasPowerup;
    [SerializeField] float powerupDuration;
    [SerializeField] GameObject playerProjectilePrefab;
    [SerializeField] GameObject powerupIndicator;
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip collisionSound;
    [SerializeField] AudioClip powerupSound;
    bool isOnGround = true;
    float boundaryRange = 19.5f;
    Rigidbody playerRb;
    GameManager gameManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; //This is optional to modify the gravity. 1 = default gravity
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        powerupIndicator.gameObject.transform.position = transform.position + new Vector3(0, -0.25f, 0);
        //When this was in FixedUpdate instead, the player was able to move around within the indicator circle
    }

    void FixedUpdate() //Use FixedUpdate instead of Update for physics related calculations
    {
        if (gameManager.isGameActive == true)
        {
            PhysicsMovement(); //I like to split these out into my own functions to keep code clean and readable
            Jump();
            PlayerBoundaries();
            FireProjectile();
        }

        if (gameManager.isGameActive == false)
        {
            playerRb.velocity = new Vector3(0, 0, 0);
        }
    }

    void PhysicsMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.right * horizontalInput * speed);
        playerRb.AddForce(Vector3.forward * verticalInput * speed);
    }

    void Jump()
    //TODO This jump motion is not as smooth as I'd like
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    void PlayerBoundaries()
    {
        if (transform.position.x < -boundaryRange)
        {
            transform.position = new Vector3(-boundaryRange, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.right, ForceMode.Impulse);
        }

        if (transform.position.x > boundaryRange)
        {
            transform.position = new Vector3(boundaryRange, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.left, ForceMode.Impulse);
        }

        if (transform.position.z < -boundaryRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundaryRange);
            playerRb.AddForce(Vector3.forward, ForceMode.Impulse);
        }

        if (transform.position.z > boundaryRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryRange);
            playerRb.AddForce(Vector3.back, ForceMode.Impulse);
        }
    }

    void FireProjectile()
    //TODO Eventually, I can replace this with an object pooler if I want
    //TODO There's something in the code here that is making projectiles curve in the direction of player movement after firing. Revisit this after I've done more training.
    {
        if (hasPowerup == true)
        {
            StartCoroutine(PowerupCountdownRoutine());

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Instantiate(playerProjectilePrefab, transform.position, playerProjectilePrefab.transform.rotation);
                playerAudio.PlayOneShot(shootSound, 1.0f);
            }
        }
    }

    IEnumerator PowerupCountdownRoutine()
    //TODO Powerup duration should reset if you pick up a powerup while you already have a powerups
    {
        yield return new WaitForSeconds(powerupDuration);
        hasPowerup = false;
        GetComponent<Renderer>().material.color = Color.white;
        powerupIndicator.gameObject.SetActive(false);
        StopAllCoroutines(); //Added this, otherwise picking up a new powerup within 5 secounds of the previous one didn't work
    }

    void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

        if (collision.gameObject.CompareTag("EnemyRed"))
        {
            Debug.Log("Player has collided with enemy.");
            playerAudio.PlayOneShot(collisionSound, 1.0f);
        }

        //if (collision.gameObject.CompareTag("EnemyOrange"))
        //No rigidbody attached, so this code won't do anything
        //{
        //    Debug.Log("Player has collided with enemy.");
        //    playerAudio.PlayOneShot(collisionSound, 1.0f);
        //}

        if (collision.gameObject.CompareTag("EnemyGreen"))
        {
            Debug.Log("Player has collided with enemy.");
            playerAudio.PlayOneShot(collisionSound, 1.0f);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Debug.Log("Player picked up powerup.");
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            Destroy(other.gameObject);
            hasPowerup = true;
            GetComponent<Renderer>().material.color = Color.grey;
            powerupIndicator.gameObject.SetActive(true);
        }
    }
}
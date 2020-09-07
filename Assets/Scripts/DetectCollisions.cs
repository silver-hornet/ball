using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyDestroyParticle;
    [SerializeField] AudioClip explosionSound;
    AudioSource playerAudio;
    GameManager gameManager;

    void Start()
    {
        enemyDestroyParticle = GetComponent<ParticleSystem>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("EnemyOrange"))
        {
            Destroy(other.gameObject);
            enemyDestroyParticle.Play();
            playerAudio.PlayOneShot(explosionSound, 1.0f);
            gameManager.UpdateScore(5);
        }

        if (other.gameObject.CompareTag("EnemyRed"))
        {
            Destroy(other.gameObject);
            enemyDestroyParticle.Play();
            playerAudio.PlayOneShot(explosionSound, 1.0f);
            gameManager.UpdateScore(10);
        }

        if (other.gameObject.CompareTag("EnemyGreen"))
        {
            Destroy(other.gameObject);
            enemyDestroyParticle.Play();
            playerAudio.PlayOneShot(explosionSound, 1.0f);
            gameManager.UpdateScore(20);
        }

        //Alternatively, if I used the above code in a script that is attached
        //to the enemies themselves, then I could achieve the same with less
        //code and fewer if statements (and wouldn't need separate tags). I would just create an int PointValue variable, then pass
        //through PointValue as a parameter in gameManager.UpdateScore()
    }
}
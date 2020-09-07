using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] GameObject doorObject;

    void Start()
    {
        doorObject = GameObject.Find("Door");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorObject.transform.Translate(0, 2, 0); //TODO In future, make this door movement smoother
            gameObject.SetActive(false); //TODO In future, leave the switch in place, but don't allow it to trigger the door again
        }
    }
}

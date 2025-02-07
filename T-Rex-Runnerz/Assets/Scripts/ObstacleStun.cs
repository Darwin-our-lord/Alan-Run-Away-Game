using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStun : MonoBehaviour
{
    //AudioSource source;
    //[SerializeField] AudioClip hit;
    GameObject controller;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            controller = GameObject.FindGameObjectWithTag("MainCamera");
            controller.GetComponent<CameraController>().StartCoroutine("Shaking");
            //source.PlayOneShot(hit, 1f);

            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(4, other.gameObject.GetComponent<Rigidbody>().velocity.y, other.gameObject.GetComponent<Rigidbody>().velocity.z);
        }
    }
}

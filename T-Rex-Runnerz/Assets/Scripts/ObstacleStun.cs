using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStun : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}

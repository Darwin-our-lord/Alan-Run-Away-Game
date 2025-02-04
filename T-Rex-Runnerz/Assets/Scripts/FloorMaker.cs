using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorMaker : MonoBehaviour
{
    public GameObject floorPrefab;
    int hitTimes;
    GameObject clone;

    void Awake()
    {
        hitTimes = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(hitTimes == 5)   
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            if(hitTimes == 0)
            {
                clone = Instantiate(floorPrefab, transform.position + new Vector3(50, 0, 0), Quaternion.identity);
                clone.name = "Floor";
            }
            hitTimes++;
        }
    }
}

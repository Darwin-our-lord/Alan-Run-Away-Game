using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject lastObs;

    GameObject player;

    public float lastObsLocation;

    // Start is called before the first frame update
    void Start()
    {
        lastObs = this.gameObject;
        player = GameObject.FindWithTag("Player1");

        CreateObstacle();

    }

    void CreateObstacle()
    {
        GameObject clone = Instantiate(obstacles[Random.Range(0,obstacles.Length)],new Vector3(lastObs.transform.position.x + Random.Range(1,40), -3.9f,0),Quaternion.identity);


        lastObs = clone;
        lastObsLocation = clone.transform.position.x;
        if (clone.transform.position.x < player.transform.position.x + 100)
        {

            CreateObstacle();
        }
    }
    void Update()
    {
        if(lastObsLocation < player.transform.position.x + 100)
        {
            CreateObstacle();
        }
    }


}

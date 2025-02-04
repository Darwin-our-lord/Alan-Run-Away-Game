using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;

    GameObject player;

    public float lastObsLocation;

    // Start is called before the first frame update
    void Start()
    {
        lastObsLocation = this.gameObject.transform.position.x;
        player = GameObject.FindWithTag("Player1");

        CreateObstacle();

    }

    void CreateObstacle()
    {
        GameObject clone = Instantiate(obstacles[Random.Range(0,obstacles.Length)],new Vector3(lastObsLocation + Random.Range(1,40), -3.9f,0),Quaternion.identity);

        lastObsLocation = clone.transform.position.x;
        if (clone.transform.position.x <= player.transform.position.x + 100)
        {

            CreateObstacle();
        }
    }
    void Update()
    {
        if(lastObsLocation <= player.transform.position.x + 100)
        {
            CreateObstacle();
        }
    }


}

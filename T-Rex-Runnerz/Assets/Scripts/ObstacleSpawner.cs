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
        int random = Random.Range(0, obstacles.Length);

        float location = 0;

        if (random == 0) location=-4.2f;
        if (random == 1) location = -3.8f;

        GameObject clone = Instantiate(obstacles[random], new Vector3(lastObsLocation + Random.Range(1, 20), location ,0),Quaternion.identity);

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

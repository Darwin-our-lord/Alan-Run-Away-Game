using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject lastObs;

    GameObject player;

    public bool isDoing;

    // Start is called before the first frame update
    void Start()
    {
        lastObs = this.gameObject;
        player = GameObject.FindWithTag("Player1");

        CreateObstacle();
        isDoing = true;
    }

    void CreateObstacle()
    {
        
        GameObject clone = Instantiate(obstacles[Random.Range(0,obstacles.Length)],new Vector3(lastObs.transform.position.x + Random.Range(15,50), -3.9f,0),Quaternion.identity);


        lastObs = clone;
        if (clone.transform.position.x < player.transform.position.x + 100)
        {
            
            CreateObstacle();
        }
        else
        {
            isDoing = false;
        }

    }
    void Update()
    {
        if(lastObs.transform.position.x < player.transform.position.x + 100 && !isDoing)
        {
            isDoing = true;
            CreateObstacle();
        }
    }


}

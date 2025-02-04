using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cameraM;


    GameObject player1;
    GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        cameraM = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        float zoomScale = (player1.gameObject.transform.position.x - player2.gameObject.transform.position.x) / 3;

        if (zoomScale < 0)
        {
            zoomScale = -zoomScale;
        }
        if (zoomScale < 3)
        {
            zoomScale = 3;
        }
        if (zoomScale > 12)
        {
            zoomScale = 12;
        }
        cameraM.orthographicSize = zoomScale;



        Vector3 targetpos = (player1.transform.position + player2.transform.position)/2;
        if (zoomScale < 30) targetpos.x += 1f;
        if (zoomScale > 30) targetpos.x += 3f;
        if (zoomScale > 47) targetpos.x += 5f;

        targetpos = new Vector3(targetpos.x, targetpos.y, -10);

        transform.position = targetpos;

    }
}

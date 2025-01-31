using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb; 
    Animator ani;
    SpriteRenderer rend;

    public bool isPlayer1;

    public GameObject winText;

    float maxdistance = 50;
    float walkSpeed = 0.01f;
    float jumpSpeed = 7;

    public bool canJump;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        float distance = player1.gameObject.transform.position.x - player2.gameObject.transform.position.x;
 
        if (distance > maxdistance || distance < -maxdistance)
        {
            bool winnerplayer1 = player1.gameObject.transform.position.x > player2.gameObject.transform.position.x;
            Win(winnerplayer1);
        }

        
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.D)) 
            {
                //rb.AddForce(new Vector3(walkSpeed, rb.velocity.y, rb.velocity.z));
                rb.velocity += new Vector3(walkSpeed,0,0);

            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
                canJump = false;
            }
        }

        if (!isPlayer1)
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                //rb.AddForce(new Vector3(walkSpeed, rb.velocity.y, rb.velocity.z));
                rb.velocity += new Vector3(walkSpeed, 0, 0);

            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && canJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
                canJump = false;
            }
        }

        if (rb.velocity.x < 3 && !Input.GetKey(KeyCode.D) && isPlayer1 || rb.velocity.x < 3 && !Input.GetKey(KeyCode.A) && !isPlayer1)
        {
            rb.velocity = new Vector3(2, rb.velocity.y, rb.velocity.z);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor") && !canJump)
        {
            canJump = true;
        }
    }

    public void Win(bool winnerPlayer1)
    {
        winText.SetActive(true);

        


        if (winnerPlayer1) winText.GetComponent<TMP_Text>().text = "player one won..(red)";
        if (!winnerPlayer1) winText.GetComponent<TMP_Text>().text = "player two won..(blue)";
    }
}

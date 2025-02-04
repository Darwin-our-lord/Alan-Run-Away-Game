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
    public GameObject winUI;
    public GameObject bananaPrefab;

    float maxdistance = 50;
    float walkSpeed = 0.02f;
    float jumpSpeed = 7f;

    bool canJump;
    bool canThrow;
    Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        StartCoroutine(waitAndBanana());
        startColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            
            if (Input.GetKey(KeyCode.Space) && canJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
                canJump = false;
            }
            if (Input.GetKey(KeyCode.F) && canThrow)
            {
                Instantiate(bananaPrefab, new Vector3(transform.position.x, -4.45f, 0), Quaternion.identity);
                canThrow = false;
                StartCoroutine(waitAndBanana());
            }

        }

        if (!isPlayer1)
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                //rb.AddForce(new Vector3(walkSpeed, rb.velocity.y, rb.velocity.z));
                rb.velocity += new Vector3(walkSpeed, 0, 0);

            }

            if (Input.GetKey(KeyCode.LeftShift) && canJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
                canJump = false;
            }
            if (Input.GetKey(KeyCode.S) && canThrow)
            {
                Instantiate(bananaPrefab, new Vector3(transform.position.x, -3.65f, 1), Quaternion.identity);
                canThrow = false;
                StartCoroutine(waitAndBanana());

            }
        }

        if (rb.velocity.x < 3)
        {
            rb.velocity = new Vector3(4, rb.velocity.y, rb.velocity.z);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor") && !canJump)
        {
            canJump = true;
        }
    }

    IEnumerator waitAndBanana()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<SpriteRenderer>().color= Color.yellow;
        canThrow = true;
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<SpriteRenderer>().color = startColor;

    }


    public void Win(bool winnerPlayer1)
    {
        winUI.SetActive(true);
        Time.timeScale = 0;
        


        if (winnerPlayer1) winText.GetComponent<TMP_Text>().text = "player one won..";
        if (!winnerPlayer1) winText.GetComponent<TMP_Text>().text = "player two won..";
    }
}

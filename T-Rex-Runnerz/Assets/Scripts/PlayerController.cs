using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb; 
    Animator ani;
    SpriteRenderer rend;
    //AudioSource source;
    //[SerializeField] AudioClip jump;
    //[SerializeField] AudioClip step;

    public bool isPlayer1;

    public TextMeshProUGUI winText;
    public GameObject winUI;
    public GameObject bananaPrefab;

    float maxdistance = 35;
    float walkSpeed = 0.02f;
    float jumpSpeed = 7f;
    float jumpDivid = 1.2f;

    bool canJump;
    bool someoneWon = false;
    bool canThrow;
    Color startColor;
    [SerializeField] private string[] scenesToLoad;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        //source = GetComponent<AudioSource>();

        StartCoroutine(waitAndBanana());
        startColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        winUI.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        float distance = player1.gameObject.transform.position.x - player2.gameObject.transform.position.x;

        ani.SetFloat("Speed", rb.velocity.x/5);
 
        if (distance > maxdistance || distance < -maxdistance)
        {
            bool winnerplayer1 = player1.gameObject.transform.position.x > player2.gameObject.transform.position.x;
            Win(winnerplayer1);
            someoneWon = true;
        }
        
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.Space)) 
            {
                //rb.AddForce(new Vector3(walkSpeed, rb.velocity.y, rb.velocity.z));
                rb.velocity += new Vector3(walkSpeed,0,0);

            }
            
            if (Input.GetKey(KeyCode.S) && canJump && !ani.GetBool("IsCrouch"))
            {
                //source.PlayOneShot(jump, 1f);
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
                
                canJump = false;
                ani.SetBool("InAir", true);
            }
           
            if (!Input.GetKey(KeyCode.S) && rb.velocity.y > 0.1)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / jumpDivid, rb.velocity.z);
            }

            if (Input.GetKey(KeyCode.UpArrow) && canThrow)
            {
                Instantiate(bananaPrefab, new Vector3(transform.position.x, -4.45f, 0), Quaternion.identity);
                canThrow = false;
                StartCoroutine(waitAndBanana());
            }
            
            ani.SetBool("IsCrouch", Input.GetKey(KeyCode.F));
            
            if (Input.GetKey(KeyCode.F)) rend.sortingOrder = 3;
            else rend.sortingOrder = 3;
                
        }

        if (!isPlayer1)
        {
            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //rb.AddForce(new Vector3(walkSpeed, rb.velocity.y, rb.velocity.z));
                rb.velocity += new Vector3(walkSpeed, 0, 0);

            }

            if (Input.GetKey(KeyCode.A) && canJump && !ani.GetBool("IsCrouch"))
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
                //source.PlayOneShot(jump, 1f);
                canJump = false;
                ani.SetBool("InAir", true);
            }

            if (!Input.GetKey(KeyCode.A) && rb.velocity.y > 0.1)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / jumpDivid, rb.velocity.z);
            }

            if (Input.GetKey(KeyCode.G) && canThrow)
            {
                Instantiate(bananaPrefab, new Vector3(transform.position.x, -3.65f, 1), Quaternion.identity);
                canThrow = false;
                StartCoroutine(waitAndBanana());

            }   
            ani.SetBool("IsCrouch", Input.GetKey(KeyCode.D));

            if (Input.GetKey(KeyCode.D)) rend.sortingOrder = 3;
            else rend.sortingOrder = 3;
        }

        if (rb.velocity.x < 3)
        {
            rb.velocity = new Vector3(4, 1f, rb.velocity.z);
        }


    }
    void Update()
    {
        if (someoneWon && Input.GetKeyDown(KeyCode.Space) || someoneWon && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(scenesToLoad[Random.Range(0,scenesToLoad.Length)]);
            Time.timeScale = 1f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor") && !canJump)
        {
            canJump = true;
            ani.SetBool("InAir", false);
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
    public void takeStep()
    {
        //source.PlayOneShot(step, 1f);
    }

    public void Win(bool winnerPlayer1)
    {
        winUI.SetActive(true);
        Time.timeScale = 0.03f;
        someoneWon = true;
        
        if (winnerPlayer1) winText.text = "player one won!";
        if (!winnerPlayer1) winText.text = "player two won!";
        Debug.Log(someoneWon);
    }
}

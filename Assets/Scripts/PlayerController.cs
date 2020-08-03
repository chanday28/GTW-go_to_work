using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public float jumpForce = 5f;

    public float jumpForce = 5000f;
    float countDownTime = 3f;
    [SerializeField] TextMeshProUGUI countdownText;
    //[SerializeField] GameObject rountTimer;
    private float roundTime = 0f;
    private bool goalCheck = false;
    private int goalCounter;
    [SerializeField] GameObject goalSuccesMessage;
    [SerializeField] GameObject goalFailMessage;
    //public bool isGrounded = false;
    //

    public Rigidbody2D playerRigidBody2D;
    public GameObject _player;
    public Animator animeControl;

    public TextMeshProUGUI scoreText;
    int score = 0;

    private bool countDownIsOver;//counter 3...2...1 after which you need to tap to start running
    private Vector2 playerPos;
    private int lifeCount;

    [SerializeField]
    private Canvas gameIF;

    public int maxProgress = 200, progress = 10;
    public int currentProgress;
    public ProgressBar bar;

    [SerializeField]
    private float runSpeed = 10f;
    private float rageSpeed = 20f;
    //private float distance = 5f;
    private bool isInRage = false;
    //float verticalMove = 0f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        countDownIsOver = false;
        StartCoroutine(WaitAndStart());
        lifeCount = 3;
        isInRage = false;
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        currentProgress = 0;
        bar.SetMaxGoal(maxProgress);
        bar.SetProgress(0);
        scoreText.text = score.ToString();
    }

    void MakeAProgress(int progress)
    {
        currentProgress += progress;
        bar.SetProgress(progress);
    }
    
    private void Awake()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obstacle" && isInRage == false)
        {
            if (lifeCount > 0)
            {

                 lifeCount-=1;
                 Debug.Log("Life count:" + lifeCount);
                 Destroy(collision.gameObject);
            } else if (lifeCount == 0)
            {
                isInRage = true;
                InRage();
                Destroy(collision.gameObject);
            }            
        }
        else if (collision.gameObject.tag == "obstacle" && isInRage == true)
        {
            Destroy(collision.gameObject);

                lifeCount -= 1;
                Debug.Log("Life count:" + lifeCount);
        }
        else if (lifeCount == 0)
        {
                isInRage = true;
                InRage();
        }
        
        else if (collision.gameObject.tag == "obstacle" && isInRage == true)
        {


        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entruder" + other.name);
        //currentProgress +=progress;
        MakeAProgress(currentProgress);
        score += 10;
        scoreText.text = score.ToString();
        goalCounter+=10;

    }


    void StartRun()
    {
        float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * runSpeed, yValue);
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isInRage == false)
        {            
            //playerRigidBody2D.AddForce(new Vector2(0f, 5f) , ForceMode2D.Impulse);         
            playerRigidBody2D.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);

            float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
            //transform.position = new Vector2(transform.position.x, yValue + 1f * Time.deltaTime * jumpForce);
                       
            Debug.Log("Jump");        
            //playerRigidBody2D.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);            

        }

    }

    public void InRage()
    {
        
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * rageSpeed, transform.position.y);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (countDownIsOver == true)
        {
            StartRun();
            Jump();
        }
    }

    private void Goal()
    {
        if(goalCounter == 100)
        {
            goalSuccesMessage.SetActive(true);
        } else
        {
            goalFailMessage.SetActive(true);
        }
    }

    IEnumerator WaitAndStart()
    {
        //yield return new WaitForSeconds(4f);
        //countDownIsOver = true;
        //yield return new WaitForSeconds(2f);
        while (countDownTime > 0)
        {
            countdownText.text = countDownTime.ToString("0");
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }
        countdownText.text = "GO";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        countDownIsOver = true;


        while (roundTime < 30)
        {
            yield return new WaitForSeconds(1f);
            roundTime++;
        }
        //rountTimer.SetActive(true);
        Goal();   // checks if the goal is finished
        countDownIsOver = false;   // pauses the game
        yield return new WaitForSeconds(1f);
    }
}


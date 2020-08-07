using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody2D;
    private BoxCollider2D boxCollider;
    public GameObject _player;
    private Animator animator;
    [SerializeField] private float runSpeed = 10f;
    //[SerializeField] private float jumpForce = 5000f;
    public float jumpVelocity;
        
    //private bool goalCheck = false;
    
   
    //public bool isGrounded = false;
    
       
    //RageMode    
    private float rageSpeed = 20f;
    private bool isInRage = false;
    public GameObject RageBG;
    
    private Vector2 playerPos;
    private int rageCount;


    //UI
    [SerializeField] private Canvas gameIF;
    [SerializeField] public GameObject goalSuccesMessage;
    [SerializeField] public GameObject goalFailMessage;
    private int goalCounter;


    //Countdown
    [SerializeField]public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    float countDownTime = 3f;
    private float roundTime = 0f;
    private bool countDownIsOver;//counter 3...2...1 after which you need to tap to start running
    //[SerializeField] GameObject rountTimer;
    
    //Slider
    private int maxProgress;
    private int currentProgress, progress;
    [SerializeField] private ProgressBar bar;
    

    
    void Start()
    {
        countDownIsOver = false;
        StartCoroutine(WaitAndStart());
        rageCount = 0;
        isInRage = false;
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentProgress = 0;
        maxProgress = 30;
        bar.SetMaxGoal(maxProgress);
        bar.SetProgress(0);
        progress = 10;
        scoreText.text = score.ToString();
        RageBG.SetActive(false);
    }

    void MakeAProgress(int progress)
    {
        currentProgress += progress;
        bar.SetProgress(progress);
        Debug.Log("1more rage!");
    }
    
    private void Awake()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Entruder" + other.name);
        ////currentProgress +=progress;
        //MakeAProgress(currentProgress);
        //score += 10;
        //scoreText.text = score.ToString();
        //goalCounter+=10;

        if (other.gameObject.tag == "trigger")
        {
            Debug.Log("Trigger " + other.gameObject.tag);
            rageCount++;
            score += 10;
            scoreText.text = score.ToString();
            //currentProgress += progress;
            MakeAProgress(currentProgress);
        }

        if ( other.gameObject.tag == "obstacle")
        {
            Debug.Log("Trigger " + other.gameObject.tag);
            if (isInRage == false)
            {
                //rageCount = 0;
                //Debug.Log("Rage Count reset");
            }            
        }

    }

       
    void StartRun()
    {
        // Set running animation
        animator.SetBool("isRun", true);
        animator.SetBool("isJump", false);        
        animator.SetBool("inTransition", false);
        animator.SetBool("inRageRun", false);
        
        float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * runSpeed, yValue);
                
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isInRage == false)
        {
            //Set jump animation
            animator.SetBool("isRun", false);
            animator.SetBool("isJump", true);
            animator.SetBool("inTransition", false);
            animator.SetBool("inRageRun", false);


            //playerRigidBody2D.AddForce(new Vector2(0f, 5f) , ForceMode2D.Impulse);     
            float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
            //transform.position = new Vector2(transform.position.x, yValue + 1f * Time.deltaTime * jumpForce);
                       
            Debug.Log("Jump");

            playerRigidBody2D.velocity = Vector2.up * jumpVelocity;

        } else
        {
            animator.SetBool("isJump", false);
        }

        

    }

    //private bool IsGrounded()
    //{
    //    RaycastHit2D raycast2d = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f);
    //    Debug.Log(raycast2d.collider);
    //    return raycast2d.collider != null;
       
    //}

    public void InRageRun()
    {
        if(rageCount == 4)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isJump", false);
            animator.SetBool("inTransition", false);
            animator.SetBool("inRageRun", true);
            transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * rageSpeed, transform.position.y);
            rageCount = 0;
        }
        
    }

    public void Rage()
    {
        if (rageCount == 3)
        {
            RageBG.SetActive(true);

            animator.SetBool("isRun", false);
            animator.SetBool("isJump", false);
            animator.SetBool("inTransition", true);
            animator.SetBool("inRageRun", false);
            rageCount++;
        }
    }


    void Update()
    {
        if (countDownIsOver == true)
        {
            StartRun();
            Jump();
        }
    }

    private void FixedUpdate()
    {

    }

    private void Goal()
    {
        if (goalCounter == 100)
        {
            goalSuccesMessage.SetActive(true);
        }
        else
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

    //IEnumerator WaitAndRageRun()
    //{
    //    yield return new WaitUntil()
    //}
}


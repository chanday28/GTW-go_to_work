using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidBody2D;
    //private BoxCollider2D boxCollider;
    public GameObject _player;
    private Animator animator;
    private float runSpeed = 10f, rageSpeed = 20f/* hitSpeed = 5f*/;

    //[SerializeField] private float jumpForce = 5000f;
    public float jumpVelocity;

    //private bool goalCheck = false;


    //public bool isGrounded = false;

    ManAnim manCol;
    BoxAnim boxCol;


    //RageMode    
   
    public bool isInRage = false;
    public GameObject BG,RageBG;
    public int rageCount;


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
    //[SerializeField] GameObject roundTimer;
    
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
        progress = 0;
        scoreText.text = score.ToString();
        RageBG.SetActive(false);
        BG.SetActive(true);

    }

     void Update()
    {
        CheckForRage();
        if (countDownIsOver == true && isInRage == false)
        {
            StartRun();
            Jump();
        }
    }

    private void CheckForRage()
    {
        if (rageCount < 3)
        {
            isInRage = false;
        }
        else if (rageCount == 3)
        {
            isInRage = true;
            Rage();
            RageBG.SetActive(true);
            BG.SetActive(false);
        }

        else
        {
            isInRage = false;
            RageBG.SetActive(false);
            BG.SetActive(true);
        }

    }

    void MakeAProgress(int progress)
    {
        currentProgress += progress;
        bar.SetProgress(progress);
        Debug.Log("1more rage! Rage: " +rageCount.ToString());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "trigger" && isInRage == false)
        {
            Debug.Log("Trigger " + other.gameObject.tag);
            rageCount++;
            score += 10;
            scoreText.text = score.ToString();
            progress += 10;
            MakeAProgress(progress);
        } else if(other.gameObject.tag == "trigger" && isInRage == true)
        {
            Debug.Log("ENRAGED");
            //score += 10;
            //scoreText.text = score.ToString();
        }


        if (other.gameObject.tag == "man" && isInRage == true)
        {
            manCol.FlyAwayMan();
            Debug.Log("IS IT " + other.gameObject.tag);
        }
        else if (other.gameObject.tag == "man" && isInRage == false)
        {
            if (progress > 0)
            {
                progress -= 10;
                MakeAProgress(progress);
            }

            if (score > 0)
            {
                score -= 10;
                scoreText.text = score.ToString();
            }
        }


        if (other.gameObject.tag == "boxes" && isInRage == true)
        {
            boxCol.FlyAwayBoxes();
            Debug.Log("IT IS " + other.gameObject.tag);
        }
        else if (other.gameObject.tag == " boxes" && isInRage == false)
        {
            boxCol.Dust();
            if (progress > 0)
            {
                progress -= 10;
                MakeAProgress(progress);
            }
            if (score > 0)
            {
                score -= 10;
                scoreText.text = score.ToString();
            }
        }

        //if (other.gameObject.tag == "obstacle" && isInRage == false)
        //{
        //    Debug.Log("Trigger " + other.gameObject.tag);

        //    if (progress > 0)
        //    {
        //        progress -= 10;
        //        MakeAProgress(progress);
        //    }

        //    if (rageCount > 0) { rageCount--; }
        //    if (score > 0) { score -= 10; scoreText.text = score.ToString(); }

        //    //StartCoroutine(WasHit());


        //    //rageCount = 0;
        //    //Debug.Log("Rage Count reset");
        //}

    }

       
    void StartRun()
    {
        animator.SetBool("inTransition", false);
        animator.SetBool("inRageRun", false);
        animator.SetBool("isRun", true);
        animator.SetBool("isJump", false);
        //animator.SetBool("isHit", false);

        float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * runSpeed, yValue);

                
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isInRage == false)
        {
            //Set jump animation
            animator.SetBool("inTransition", false);
            animator.SetBool("inRageRun", false);
            animator.SetBool("isRun", false);
            animator.SetBool("isJump", true);
            //animator.SetBool("isHit", false);
              
            float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
            //transform.position = new Vector2(transform.position.x, yValue + 1f * Time.deltaTime * jumpForce);
                       
            Debug.Log("Jump");

            playerRigidBody2D.velocity = Vector2.up * jumpVelocity;
            playerRigidBody2D.AddForce(new Vector2(10f, 0f), ForceMode2D.Impulse);


        } //animator.SetBool("isJump", false);

        

    }

    //private bool IsGrounded()
    //{
    //    RaycastHit2D raycast2d = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f);
    //    Debug.Log(raycast2d.collider);
    //    return raycast2d.collider != null;
       
    //}

    public void InRageRun()
    {
        StartCoroutine(UseUpRage());  

        animator.SetBool("inTransition", false);
        animator.SetBool("inRageRun", true);
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);

        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * rageSpeed, transform.position.y);

        
        
                    
    }

    public void Rage()
      {                   
        animator.SetBool("inTransition", true);
        animator.SetBool("inRageRun", false);
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);

        transform.position = new Vector2(transform.position.x, -5f);
        StartCoroutine(WaitAndRageRun());
        

    }


    //void HitTheObject()
    //{
    //    animator.SetBool("isHit", true);
    //    animator.SetBool("isRun", false);
    //    animator.SetBool("inRageRun", false);
    //    animator.SetBool("isJump", false);
    //    animator.SetBool("isHit", false);

    //    float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
    //    transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * hitSpeed, yValue);
               
    //}

         

    IEnumerator WaitAndRageRun()
    {
        yield return new WaitForSeconds(1f);
        InRageRun();
              
    }

    IEnumerator UseUpRage()
    {
        
        yield return new WaitForSeconds(2f);
        
        rageCount = 0;
        bar.SetProgress(0);
        progress = 0;
        isInRage = false;
        RageBG.SetActive(false);
        BG.SetActive(true);

    }
 
    //IEnumerator WasHit()
    //{
    //    HitTheObject();
    //    yield return new WaitForSeconds(1f);
    //}
    
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
        //roundTimer.SetActive(true);
        Goal();   // checks if the goal is finished
        countDownIsOver = false;   // pauses the game
        yield return new WaitForSeconds(1f);
    }
}


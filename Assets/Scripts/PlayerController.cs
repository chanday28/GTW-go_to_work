using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5000f;
    //
    public Rigidbody2D playerRigidBody2D;
    public GameObject _player;

    private bool countDownIsOver;//counter 3...2...1 after which you need to tap to start running
    private Vector2 playerPos;
    private int lifeCount;

    [SerializeField]
    private Canvas gameIF;

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
    }

    
    private void Awake()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "obstacle" && isInRage == false)
        {
            if (lifeCount > 0)
            {
                 lifeCount-=1;
                 Debug.Log("Life count:" + lifeCount);  
            } else if (lifeCount == 0)
            {
                isInRage = true;
                InRage();
            }            
        }
        else if (collision.gameObject.tag == "obstacle" && isInRage == true)
        {
            
        }
    }



    void StartRun()
    {
        float yValue = Mathf.Clamp(transform.position.y, -5f, 7f);
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * runSpeed, yValue);
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            //playerRigidBody2D.AddForce(new Vector2(0f, 5f) , ForceMode2D.Impulse);         
            playerRigidBody2D.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);            
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

    IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(4f);
        countDownIsOver = true;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moobment : MonoBehaviour
{
    //Sounds
    public AudioSource walk;
    public AudioClip[] walkSoundArray;
    public float stepInterval;
    private float elapsedWalkTime;
    //Movement tools
    private bool isRunning;
    public float moveSpeed = 5f;
    public SpriteRenderer Renderer;
    public Animator animator;
    private Rigidbody2D rb;
    private int direction;
    private Vector2 moveDirection;
    [HideInInspector]
    public float maxStamina = 100f;
    private float stamDrain = 20f;
    [HideInInspector]
    public float currentStamina;
    //Dashing tools
    public float dashSpeed;
    public float startDashTime;
    private float dashTime;
    public int dashCount;
    public float regenTime;
    private float elapsedRegenTime;
    public static bool isDashing;


    private void Start()
    {
        dashTime = startDashTime;
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        DashRegen();
        Dash();
        PlayerMovement();
        gunFaceMouse();
        playerFaceMouse();
        walkTimer();

        //Changing the players position
        if (direction == 0)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
    }


    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
        }
        else
        {
            Debug.Log("oop");
        }
    }

    void playerFaceMouse()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (transform.position.x < mousePos.x)
        {
            Renderer.flipX = false;

        }
        else
        {
            Renderer.flipX = true;
        }

    }

    void walkTimer()
    {
        if (moveDirection != Vector2.zero)
        {
            elapsedWalkTime += Time.deltaTime;
            if (elapsedWalkTime >= stepInterval)
            {
                walk.clip = walkSoundArray[Random.Range(0, walkSoundArray.Length)];
                walk.PlayOneShot(walk.clip);
                animator.SetBool("isWalking", true);
                elapsedWalkTime = 0f;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

    }

    void DashRegen()
    {
        if (dashCount < 3) 
        { 
            elapsedRegenTime += Time.deltaTime;
            if (elapsedRegenTime >= regenTime)
            {
                elapsedRegenTime = 0f;
                dashCount += 1;
                Debug.Log(dashCount);
            }
        }
    }

    void Dash()
    {
        if (dashCount > 0 && direction == 0 && Input.GetKeyDown(KeyCode.Space))
        {

            if (Input.GetKey("a"))
            {
                direction = 1;
                dashCount -= 1;
                Debug.Log(dashCount);
                walk.Play();
                isDashing = true;
            }
            else if (Input.GetKey("d"))
            {
                direction = 2;
                dashCount -= 1;
                Debug.Log(dashCount);
                walk.Play();
                isDashing = true;
            }
            else if (Input.GetKey("w"))
            {
                direction = 3;
                dashCount -= 1;
                Debug.Log(dashCount);
                walk.Play();
                isDashing = true;
            }
            else if (Input.GetKey("s"))
            {
                direction = 4;
                dashCount -= 1;
                Debug.Log(dashCount);
                walk.Play();
                isDashing = true;
            }
            
            
        } else {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;


                if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;   
                }
                else if (direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == 3)
                {
                    rb.velocity = Vector2.up * dashSpeed;
                }
                else if (direction == 4)
                { 
                    rb.velocity = Vector2.down * dashSpeed;
                }
            }
        }
        
    }

    void gunFaceMouse()
    {
        //Getting mouseposition
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );


        transform.GetChild(0).gameObject.transform.up = direction;
        //transform.up = direction;

    }

    void PlayerMovement()
    {
        //Movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        //Check If Sprinting
        if (moveDirection != Vector2.zero && Input.GetKey(KeyCode.LeftShift))
            {
            if (currentStamina > 0)
            {
                currentStamina = currentStamina - (stamDrain * Time.deltaTime);
                moveSpeed = 8f;
                stepInterval = .3f;
                animator.SetBool("isRunning", true);
            }
            else
            {
                moveSpeed = 5f;
                stepInterval = .5f;
                animator.SetBool("isRunning", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        { 
            moveSpeed = 5f;
            stepInterval = .5f;
            animator.SetBool("isRunning", false);
        }
    }




}

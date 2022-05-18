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
    public Joystick joystick;
    public Joystick aimstick;
    public GameObject gun;
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
        //playerFaceMouse();
        walkTimer();

        //Changing the players position
        //if (direction == 0)
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
        if (aimstick.Direction != Vector2.zero && aimstick.Horizontal > 0)
        {
            Renderer.flipX = false;

        }
        else if (joystick.Direction != Vector2.zero && joystick.Horizontal > 0)
        {
            Renderer.flipX = false;
        }else
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
        //Getting touch position

        if (Input.touchCount > 0)
        {





            int i = 0;
            while (i < Input.touchCount && gun != null)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 touchDir = new Vector2(
                    touch.position.x - Camera.main.WorldToScreenPoint(transform.position).x,
                    touch.position.y - Camera.main.WorldToScreenPoint(transform.position).y
                    );

                int FID = touch.fingerId;

                if(touch.phase == TouchPhase.Began)
                {
                    var startPos = touch.position;
                    if (startPos.x < 400 && startPos.y < 300) { FID = 100; }
                }
                   

                void TouchControlsDirection()
                {
                    transform.GetChild(0).transform.up = touchDir;
                    if(touchDir.x > 0)
                    {
                        gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitR;
                        gun.GetComponent<SpriteRenderer>().flipX = false;
                        Renderer.flipX = false;
                    }
                    else
                    {
                        gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitL;
                        gun.GetComponent<SpriteRenderer>().flipX = true;
                        Renderer.flipX = true;
                    }
                }

                if (Input.touchCount == 1)
                {
                    if ((FID == 100))
                    {
                        JoystickControlsDirection();
                    }
                    else
                    {
                        TouchControlsDirection();
                    }
                }else if (Input.touchCount == 2)
                {
                    if((FID != 100))
                    {
                        TouchControlsDirection();
                    }
                }


                //transform.GetChild(0).transform.up = touchDir;
                //if (touch.fingerId != 100 && touchDir.x > 0 && gun != null)
                //{
                //    gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitR;
                //    gun.GetComponent<SpriteRenderer>().flipX = false;
                //    Renderer.flipX = false;
                //}
                //else if (touch.fingerId != 100 && touchDir.x < 0 && gun != null)
                //{
                //    gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitL;
                //    gun.GetComponent<SpriteRenderer>().flipX = true;
                //    Renderer.flipX = true;
                //}
                i++;
            }            
        }        
    }

    void JoystickControlsDirection()
    {
        transform.GetChild(0).transform.up = joystick.Direction;
        if(joystick.Direction.x > 0)
        {
            gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitR;
            gun.GetComponent<SpriteRenderer>().flipX = false;
            Renderer.flipX = false;
        }
        else
        {
            gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitL;
            gun.GetComponent<SpriteRenderer>().flipX = true;
            Renderer.flipX = true;
        }
    }




    void PlayerMovement()
    {
        float moveX;
        float moveY;
        //Movement input
        if (joystick == null)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }
        else
        {
            moveX = joystick.Horizontal;
            moveY = joystick.Vertical;
        }

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
                gunFaceMouse();
            }
            else
            {
                moveSpeed = 5f;
                stepInterval = .5f;
                animator.SetBool("isRunning", false);
                gunFaceMouse();
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

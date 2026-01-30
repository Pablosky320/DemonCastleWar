using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    /* This floats are needed for movement, normally they would placed in the movement function but 
    you need them called at the beggining so you can call them in each function */
    float x;
    float y; 
    float z;

    //This are basic statistics of the character
    public float speed = 12;
    public float runningSpeed = 20f;
    public float dashSpeed = 30f;
    public float finalSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float dashCooldown = 0.4f;
    float dashDuration = 0.8f;


    //every vector called
    Vector3 moveDirection;
    Vector3 dashDirection;
    Vector3 velocity;
    Vector3 move;

    //gameobjects / layers needed
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    //bools for when the character can do things

    [SerializeField] bool canDash;
    [SerializeField] bool canRun;
    [SerializeField] bool canCrouch;

    [SerializeField] bool isRunning;
    [SerializeField] bool isDashing;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isCrouching;


    void Start() 
    {
        speed = 12f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            if (isRunning == true)


            if (isDashing == true)
                canRun == false

            if (isCrouching == true)
                canDash == false
                canRun == false

        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // gravity, because the character doesn't use rigidbody

        if (isGrounded)
        {
            if (isCrouching)
            {
                canDash = false;
                canRun = false;
            }
            if (isRunning)
            {
                canDash = true;
                canCrouch = true;
            }
            if (isDashing)
            {
                canRun = false;
                canCrouch = false;
            }
        }


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Movement equations
        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (isGrounded == true) 
        {
            //This is the crouching logic
            if (Input.GetKeyDown(KeyCode.LeftControl) && canCrouch == true)
            {
                isCrouching = true;
                controller.height = 1.2f;
                speed = 6f;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching == true)
            {
                isCrouching = false;
                controller.height = 2f;
                speed = 12f;
            }
            
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = true;
                speed = 20f;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift) && (isRunning == true))
            {
                isRunning = false;
                canDash = true;
                speed = 12f;
            }
        }
    }


    IEnumerator Dashing()
    {
        isRunning = false
        isCrouching = false
        canDash = false;
        isDashing = true;

        dashDirection = move.normalized;
        
        float elapsed = 0f;

        if (dashDirection.magnitude != 0f)
        {
            while (elapsed < dashDuration)
            {
                controller.Move(dashDirection * dashSpeed * Time.deltaTime);
                elapsed += Time.deltaTime;             
                yield return null; // espera un frame        
            }
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        speed = 12;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed;
    public float dashSpeed = 25f;
    public float finalSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float dashCooldown = 0.4f;

    public Transform groundCheck;
    public LayerMask groundMask;

    private bool canDash;
    [SerializeField] bool isDashing;
    [SerializeField] bool isGrounded;

    Vector3 moveDirection;
    Vector3 dashDirection;
    Vector3 velocity;
    Vector3 move;

    [SerializeField] bool isCrouching;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Movement();

        if (isGrounded == true) 
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = true;
                controller.height = 1.2f;
                speed = 6f;
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isCrouching = false;
                controller.height = 2f;
                speed = 12f;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if ()

                    StartCoroutine(Dashing());
            }
        }
    }

    void Movement()
    {

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Run()
    {

    }

    IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;

        dashDirection = move.normalized;
        if (dashDirection.magnitude != 0f)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}

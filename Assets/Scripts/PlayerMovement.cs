using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed;
    public float finalSpeed;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    [SerializeField] bool isCrouching;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Movement();

        while (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = true;
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

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


    void Crouch()
    {   
        isCrouching = true;
        controller.height = 1.2f;
    }
}

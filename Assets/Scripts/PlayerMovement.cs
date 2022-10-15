using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    //PlayerJump pJump;

    [Header("Movement")]
    float movementSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float shootSpeed;
    [SerializeField] float airMultiplier;
    Vector3 moveDirection;
    [SerializeField] float horizontalMovement, verticalMovement;
    [SerializeField] Transform orientation;
    [SerializeField] float groundDrag, airDrag;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 5f;
    bool canJump => (isGrounded && Input.GetKeyDown(KeyCode.Space));

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    public bool isGrounded { get; private set; }
   


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        CheckGround();
        PlayerInput();
        Jump();

        if (!isGrounded)
        {
            JumpGravity();
        }
        else
        {
            NormalGravity();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        
    }

    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void MovePlayer()
    {
        ControlDrag();
        movementSpeed = walkSpeed;
        rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Acceleration);
        //rb.velocity = moveDirection.normalized * movementSpeed;
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }


    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
    }

    void JumpGravity()
    {
        if(rb.velocity.y <= -0.05f)
        {
            GetComponent<CustomGravity>().gravityScale = GetComponent<CustomGravity>().fallingGravityScale;
        }
    }

    void NormalGravity()
    {
        GetComponent<CustomGravity>().gravityScale = GetComponent<CustomGravity>().normalGravityScale;
    }

}

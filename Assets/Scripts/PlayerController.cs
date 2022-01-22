using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveForce;
    public float maxSpeed;

    private Rigidbody2D m_RigidBody;

    private float m_CurrentSpeed;

    public float jumpForce;
    public float wallJumpForce;
    public float parachuteSpeed;
    public float waterfallSpeed;
    private float m_MoveInput;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float friction = 1;

    private bool walljumping;
    private bool parachuting = false;
    private bool waterfall = false;


    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        Vector2 m_MoveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        m_CurrentSpeed = m_MoveDirection.x * moveForce * Time.deltaTime;
        //Debug.Log("Current speed : " + m_CurrentSpeed);

        m_RigidBody.AddForce(m_MoveDirection * moveForce * Time.deltaTime);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        m_MoveInput = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); //- friction*Time.deltaTime); 
        //rb.AddRelativeForce(new Vector2(moveInput * speed, 0));

        if (waterfall)
        {
            if (m_RigidBody.velocity.y > -Mathf.Abs(waterfallSpeed))
                m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, -Mathf.Abs(waterfallSpeed));
            if (parachuting) parachuting = false;
        }


        if (parachuting)
        {
            if (isGrounded)
                parachuting = false;
            else if (m_RigidBody.velocity.y < -Mathf.Abs(parachuteSpeed))
                m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, -Mathf.Abs(parachuteSpeed));
        }



        if (facingRight == false && m_MoveInput > 0)
        {
            Flip();
        } else if(facingRight == true && m_MoveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if ((Input.GetKey("w") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && (isGrounded || walljumping))
        {
            m_RigidBody.velocity = Vector2.up * jumpForce;
            if (walljumping)
            {
                m_RigidBody.velocity += Vector2.left * wallJumpForce;
                walljumping = false;
            }
        }

        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void StartWalljump()
    {
        walljumping = true;
    }
    public void StopWalljump()
    {
        walljumping = false;
    }
    public void StartParachute()
    {
        parachuting = true;
    }
    public void StartWater()
    {
        waterfall = true;
    }
    public void StopWater()
    {
        waterfall = false;
    }
}

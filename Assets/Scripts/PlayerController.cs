using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveForce;
    public float maxSpeed;

    private Rigidbody2D m_RigidBody;

    private Vector2 m_CurrentSpeed;

    private float m_PreJumpVelocity;
    private Vector2 m_PreJumbVector;

    public float jumpForce;
    public float wallJumpForce;
    public float parachuteSpeed;
    public float waterfallSpeed;
    private float m_MoveInput;

    private bool facingRight = true;

    private bool m_IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float landingMomentumDivider;
    private float m_Timer;
    private bool m_JumpState = false;
    private bool m_IsLanding = false;

    public float jumpHorizontalSpeed;

    private bool m_IsBouncing = false;

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

        m_CurrentSpeed.x = m_MoveDirection.x * moveForce * Time.deltaTime;
        //Debug.Log("Current speed : " + m_CurrentSpeed);

        if(Mathf.Abs(m_RigidBody.velocity.x)<= maxSpeed)
        {
            if (m_JumpState)
            {
                m_RigidBody.AddForce(jumpHorizontalSpeed * m_CurrentSpeed);
            }
            else
            {
                m_RigidBody.AddForce(m_CurrentSpeed);
            }
            
        }

        m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        JumpRisingEdgeDetection(m_CurrentSpeed);

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
            if (m_IsGrounded)
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

    private void JumpRisingEdgeDetection(Vector2 currentSpeed)
    {
        if(!m_IsGrounded && !m_JumpState)
        {
            m_JumpState = true;
            m_PreJumbVector = currentSpeed;
        }

        else if(m_IsGrounded && m_JumpState)
        {
            m_JumpState = false;
            m_IsLanding = true;
        }

        else if(m_IsGrounded && m_IsLanding && !m_IsBouncing)
        {
            m_PreJumbVector.x = m_PreJumbVector.x / landingMomentumDivider;
            m_RigidBody.velocity = m_PreJumbVector;
            m_IsLanding = false;
        }

        if(m_IsGrounded && m_IsLanding && m_IsBouncing)
        {
            m_IsLanding = false;
            m_IsBouncing = false;
            Debug.Log("Ok done bouncing");
        }
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space)) && (m_IsGrounded || walljumping))
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bumper"))
        {
            m_IsBouncing = true;
            Debug.Log("I am bouncing");
        }
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

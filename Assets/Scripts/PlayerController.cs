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

    private Vector2 m_PreJumbVector;

    public float jumpForce;
    public float jumpFlipModifier;
    public float wallJumpForce;
    public float parachuteSpeed;
    public float waterfallSpeed;
    private float m_MoveInput;

    private bool facingRight = true;

    private bool m_IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool m_HasHitWall = false;
    public Transform wallCheckUp;
    public Transform wallCheckDown;

    public float landingMomentumDivider;
    private bool m_JumpState = false;
    private bool m_IsLanding = false;

    public float jumpHorizontalSpeed;

    private bool m_IsBouncing = false;

    private bool walljumping;
    private bool parachuting = false;
    private bool waterfall = false;
    private bool m_ParachuteIsOn = false;

    private bool m_IsWalking;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        LevelManager.instance.currentPlayer = gameObject;
        Debug.Log("According to player, ground is : " + whatIsGround);

    }

    void FixedUpdate()
    {
        if (LevelStateManager.Instance.m_currentState == LevelStateManager.Instance.playingState)
        {



            //Debug.Log(m_IsWalking);
            Vector2 m_MoveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

            m_CurrentSpeed.x = m_MoveDirection.x * moveForce * Time.deltaTime;
            //Debug.Log("Current speed : " + m_CurrentSpeed);

            if(Mathf.Abs(m_RigidBody.velocity.x)<= maxSpeed && !m_HasHitWall)
            {
                if (m_JumpState)
                {
                    m_RigidBody.AddForce(jumpHorizontalSpeed * m_CurrentSpeed);
                }
                else
                {
                    m_RigidBody.AddForce(m_CurrentSpeed);
                    m_IsWalking = true;
                }
            
            } 
            
            else if (Mathf.Abs(m_RigidBody.velocity.x)>= maxSpeed && !m_JumpState)
            {

                Debug.Log("I got into the flip speed condition");

                Vector2 v = m_RigidBody.velocity;
                if (facingRight)
                {
                    v.x = maxSpeed;
                }
                else if (!facingRight)
                {
                    v.x = (-1) * maxSpeed;
                }
                m_RigidBody.velocity = v;
            }

            if(Mathf.Abs(m_RigidBody.velocity.x) <= 1.5)
            {
                m_IsWalking = false;
            }

            m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            if (Physics2D.OverlapCircle(wallCheckUp.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(wallCheckDown.position, checkRadius, whatIsGround))
            {
                m_HasHitWall = true;
            }
            else
                m_HasHitWall = false;

            if (m_HasHitWall)
            {
                Debug.Log("Wall hit");
            }

            JumpRisingEdgeDetection(m_CurrentSpeed);

            m_MoveInput = Input.GetAxis("Horizontal");

            if (facingRight == false && m_MoveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && m_MoveInput < 0)
            {
                Flip();
            }

            if (waterfall)
            {
                if (m_RigidBody.velocity.y > -Mathf.Abs(waterfallSpeed))
                    m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, -Mathf.Abs(waterfallSpeed));
                if (parachuting) parachuting = false;
            }

            if (parachuting)
            {
                if (m_IsGrounded)
                {
                    parachuting = false;
                    m_ParachuteIsOn = false;
                }
                else if ((m_RigidBody.velocity.y < -Mathf.Abs(parachuteSpeed)) && m_ParachuteIsOn)
                    m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, -Mathf.Abs(parachuteSpeed));
            }


        }
    }

    private void JumpRisingEdgeDetection(Vector2 currentSpeed)
    {
        if(!m_IsGrounded && !m_JumpState)
        {
            m_JumpState = true;
            m_PreJumbVector = currentSpeed;
            m_IsWalking = false;
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
        }
    }

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space)) && (m_IsGrounded || walljumping || m_ParachuteIsOn))
        {

            if (m_ParachuteIsOn)
            {
                m_ParachuteIsOn = false;
            }
            else
            {
                m_RigidBody.velocity = Vector2.up * jumpForce;

                //SoundManager.Instance.PlaySound("jump");

                if (walljumping)
                {
                    m_RigidBody.velocity += Vector2.left * wallJumpForce;
                    walljumping = false;
                }
            }
        }
        else if((Input.GetKeyDown(KeyCode.Space)) && parachuting && (!m_ParachuteIsOn))
        {
            m_ParachuteIsOn = true;
        }

        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        if (m_JumpState)
        {
            m_RigidBody.AddForce(jumpFlipModifier * m_CurrentSpeed);
            m_PreJumbVector.x = m_PreJumbVector.x * (-1);
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
        m_ParachuteIsOn = true;
    }
    public void StartWater()
    {
        waterfall = true;
    }
    public void StopWater()
    {
        waterfall = false;
    }


    //Les getterz
    public bool GetJumpState()
    {
        return m_JumpState;
    }

    public bool GetLandingState()
    {
        return m_IsLanding;
    }

    public bool GetWalkingState()
    {
        return m_IsWalking;
    }

    public bool GetWallJumpState()
    {
        return walljumping;
    }

    public bool GetWaterState()
    {
        return waterfall;
    }

    public bool GetParachuteState()
    {
        return parachuting;
    }
}

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
    private float m_MoveInput;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float friction = 1;


    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        Vector2 m_MoveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        m_CurrentSpeed = m_MoveDirection.x * moveForce * Time.deltaTime;
        Debug.Log("Current speed : " + m_CurrentSpeed);

        m_RigidBody.AddForce(m_MoveDirection * moveForce * Time.deltaTime);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        m_MoveInput = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); //- friction*Time.deltaTime); 
        //rb.AddRelativeForce(new Vector2(moveInput * speed, 0));


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


        if ((Input.GetKey("w") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            m_RigidBody.velocity = Vector2.up * jumpForce;
        }

        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}

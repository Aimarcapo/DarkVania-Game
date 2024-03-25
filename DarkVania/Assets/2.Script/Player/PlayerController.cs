using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    float velX, velY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    Animator animator;
    public static PlayerController instance;
    bool esObstaculizado=false;
    float obstacleSide;
    public Vector3 respawnPoint;
    public GameObject fallDetector;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        esObstaculizado = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded)
        {

            animator.SetBool("Jump", false);
            Attack();
        }
        else
        {

            animator.SetBool("Jump", true);
            JumpAttack();

        }
        JumpAttack();
        FlipCharacter();
        fallDetector.transform.position = new Vector2(transform.position.x,fallDetector.transform.position.y);

    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

    }
    public void Idle()
    {
        animator.SetBool("Attack", false);
    }
    public void Attack()
    {
        // Si está en el aire, cancelar la animación de salto y poner la de ataque.
        if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {
            //Attack();

            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);

        }

    }

    public void Jump()
    {
        if ((Input.GetButton("Jump") || Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight + 1f);
        }
    }

    public void JumpAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Attack();

            animator.SetBool("JumpAttack", true);
        }
        else
        {
            animator.SetBool("JumpAttack", false);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            esObstaculizado = true;
             obstacleSide = Mathf.Sign(transform.position.x - collision.gameObject.transform.position.x);
        }
        else
        {
            esObstaculizado = false;
        }
    }
    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;
        //Debug.Log(velX + " " + velY);
        
        if (!esObstaculizado)
        {
            rb.velocity = new Vector2(velX * speed, velY);
            if (rb.velocity.x != 0)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        else
        {
           
            rb.velocity = new Vector2(obstacleSide * speed, -jumpHeight+1f);
            //if(velX)
            animator.SetBool("Walk", false);
        }
        
    }

    public void FlipCharacter()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
    }

}

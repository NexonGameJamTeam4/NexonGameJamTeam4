using BackEnd.RealTime;
using System.Collections;
using UnityEngine;

public class PlayerController : CreatureState
{
    /*public float maxSpeed = 5f;
    public float jumpForce = 10f;
    public float backgroundSpeed;

    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Coroutine jumpBuffer;

    private State state;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        state = State.Idle;
    }

    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            state = State.Jump;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rb.velocity.x > maxSpeed || rb.velocity.x > 0)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            backgroundSpeed = rb.velocity.x / 2;
            if (state != State.Jump)
                state = State.Run;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.velocity.x < -maxSpeed || rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            backgroundSpeed = rb.velocity.x / 2;
            if (state != State.Jump)
                state = State.Run;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            backgroundSpeed = rb.velocity.x / 2;
        }

        if (h == 0)
        {
            if(state != State.Jump)
                state = State.Idle;
            rb.velocity = new Vector2(0, rb.velocity.y);
            backgroundSpeed = 0;
        }
        AnimationUpdate();
    }

    public override void AnimationUpdate()
    {
        switch (state)
        {
            case State.Idle:
                anim.Play("CharacterIdle");
                break;
            case State.Run:
                anim.Play("CharacterRun");  
                break;
            case State.Jump:
                anim.Play("CharacterJump");
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            state = State.Idle;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(jumpBuffer != null) StopCoroutine(jumpBuffer);
            jumpBuffer = StartCoroutine(CoJumpBuffer());
        }
    }

    IEnumerator CoJumpBuffer()
    {
        yield return new WaitForSeconds(0.1f);

        jumpBuffer = null;
        isGrounded = false;
    }*/

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private State state;
    private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<Transform>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            state = State.Jump;
            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (state != State.Jump && rb.velocity.x != 0)
            state = State.Run;
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
            state = State.Idle;

        Flip();
        AnimationUpdate();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public override void AnimationUpdate()
    {
        switch (state)
        {
            case State.Idle:
                anim.Play("CharacterIdle");
                break;
            case State.Run:
                anim.Play("CharacterRun");
                break;
            case State.Jump:
                anim.Play("CharacterJump");
                break;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.2f);
        isJumping = false;
    }

    public void Dead()
    {
        //TODO : Defeat, dead motion
    }
}

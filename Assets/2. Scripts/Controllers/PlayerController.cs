using System.Collections;
using UnityEngine;

public class PlayerController : CreatureState
{
    private float horizontal;
    public float speed = 8f;
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
        if (state == State.Die) return;

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
            case State.Die:
                anim.Play("CharacterDead");
                rb.velocity = (Vector2.up * 5f);
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
        state = State.Die;
        AnimationUpdate();
    }
}

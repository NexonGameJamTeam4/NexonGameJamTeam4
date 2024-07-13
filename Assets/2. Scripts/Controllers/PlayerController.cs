using System.Collections;
using UnityEngine;

public class PlayerController : CreatureState
{
    public float maxSpeed = 5f;
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
    }
}

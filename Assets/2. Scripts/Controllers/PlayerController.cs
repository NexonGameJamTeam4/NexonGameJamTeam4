using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpPower;

    Rigidbody2D rb;
    Vector2 jumpVector = new Vector2();
    bool isGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpVector = jumpPower * Vector2.up;
        isGround = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Debug.Log("Space");
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}

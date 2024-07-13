using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public enum BlockType
    {
        Rock,
        Leaf,
        Dandelion,
        Papercup,
    }

    [SerializeField]
    private BlockType type;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (type)
            {
                case BlockType.Rock:
                    break;
                case BlockType.Leaf:
                    LeafMovement();
                    break;
                case BlockType.Dandelion:
                    DandelionMovement();
                    break;
                case BlockType.Papercup:
                    PapercupMovement();
                    break;
            }
        }
    }

    void LeafMovement()
    {
        StartCoroutine(LeafRun());
    }

    void DandelionMovement()
    {

    }

    void PapercupMovement()
    {

    }

    IEnumerator LeafRun()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                break;
            }
            rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * 30);
            yield return null;

        }
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public enum BlockType
    {
        Rock,
        Leaf,
        Papercup,
        Mushroom,
        Bark,
        Sap,
        Moss,
        Dandelion,
        ShatteredRock,
    }

    [SerializeField]
    private BlockType type;
    [SerializeField]
    private Sprite secondCup;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private int paperCount;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        paperCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
                case BlockType.Papercup:
                    PapercupMovement();
                    break;
                case BlockType.Mushroom:
                    break;
                case BlockType.Bark:
                    break;
                case BlockType.Sap:
                    break;
                case BlockType.Moss:
                    break;
                case BlockType.Dandelion:
                    DandelionMovement();
                    break;
                case BlockType.ShatteredRock:
                    break;
            }
        }
    }

    void LeafMovement()
    {
        StartCoroutine(CoLeaf());
    }

    void DandelionMovement()
    {
        StartCoroutine(CoDandelion());
    }

    void PapercupMovement()
    {
        if(paperCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            paperCount++;
            sprite.sprite = secondCup;
        }
    }

    IEnumerator CoLeaf()
    {
        float time = 0;
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                break;
            }
            rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * 50);
            yield return null;

        }
        Destroy(gameObject);
    }

    IEnumerator CoDandelion()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
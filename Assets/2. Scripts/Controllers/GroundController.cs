using BackEnd;
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
    private Color leafColor;
    private BoxCollider2D leafCollider;

    Coroutine coLeaf;
    Coroutine coDandelion;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        leafCollider = GetComponent<BoxCollider2D>();
        if(type == BlockType.Papercup)
            sprite = GetComponentInChildren<SpriteRenderer>();
        paperCount = 0;
    }

    private void Start()
    {
        if(type == BlockType.Leaf)
        {
            LeafMovement();
        }
        else if(type == BlockType.Dandelion)
        {
            DandelionMovement();
        }
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
                    break;
                case BlockType.Papercup:
                    PapercupMovement();
                    break;
                case BlockType.Mushroom:
                    MushroomMovement();
                    break;
                case BlockType.Bark:
                    MushroomMovement();
                    break;
                case BlockType.Sap:
                    SapMovement(collision.gameObject);
                    break;
                case BlockType.Moss:
                    break;
                case BlockType.Dandelion:
                    break;
                case BlockType.ShatteredRock:
                    MushroomMovement();
                    break;
            }
        }
    }

    void LeafMovement()
    {
        if(coLeaf == null)
            coLeaf = StartCoroutine(CoLeaf());
    }

    void MushroomMovement()
    {
        StartCoroutine(CoMushroom());
    }

    void PapercupMovement()
    {
        if(paperCount > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            paperCount++;
            sprite.sprite = secondCup;
        }
    }

    void SapMovement(GameObject player)
    {
        if (player.transform.localScale.x > 0)
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.right);
        else
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
    }

    void DandelionMovement()
    {
        if(coDandelion == null)
            StartCoroutine(CoDandelion());
    }

    IEnumerator CoLeaf()
    {
        while(true)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
            leafCollider.enabled = false;
            yield return new WaitForSeconds(3f);
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            leafCollider.enabled = true;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator CoMushroom()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator CoDandelion()
    {
        Vector3 upPosition = transform.position + Vector3.up * 5f;
        Vector3 downPosition = transform.position + Vector3.down * 5f;
        while(true)
        {
            while(Vector3.Distance(transform.position, upPosition) > 0.1f)
            {
                rb.MovePosition(upPosition);
            }

            yield return new WaitForSeconds(2f);

            while (Vector3.Distance(transform.position, downPosition) > 0.1f)
            {
                rb.MovePosition(downPosition);
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
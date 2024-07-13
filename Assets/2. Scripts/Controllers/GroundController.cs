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
    private CapsuleCollider2D leafCollider;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        leafCollider = GetComponent<CapsuleCollider2D>();
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
                    MushroomMovement();
                    break;
                case BlockType.Bark:
                    break;
                case BlockType.Sap:
                    break;
                case BlockType.Moss:
                    break;
                case BlockType.Dandelion:
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
}
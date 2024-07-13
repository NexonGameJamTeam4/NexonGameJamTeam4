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
    Coroutine coSap;

    Vector3 upPosition;
    Vector3 downPosition;
    bool flag;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        leafCollider = GetComponent<BoxCollider2D>();
        if(type == BlockType.Papercup)
            sprite = GetComponentInChildren<SpriteRenderer>();
        paperCount = 0;

        upPosition = transform.position + Vector3.up * 3f;
        downPosition = transform.position + Vector3.down * 3f;
        int i = Random.Range(0, 2);
        if (i == 0)
            flag = true;
        else
            flag = false;
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
        if(coSap == null)
            coSap = StartCoroutine(CoSap(player));
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
        while(true)
        {
            // 위로
            if(flag && Vector3.Distance(transform.position, upPosition) > 0.2f)
            {
                transform.Translate(Vector2.up * 2f * Time.deltaTime);
            }
            else if(flag && Vector3.Distance(transform.position, upPosition) < 0.2f)
            {
                flag = false;
            }

            yield return null;

            if(!flag && Vector3.Distance(transform.position, downPosition) > 0.2f)
            {
                transform.Translate(Vector2.down * 2f * Time.deltaTime);
            }
            else if(!flag && Vector3.Distance(transform.position, downPosition) < 0.2f)
            {
                flag = true;
            }
        }
    }

    IEnumerator CoSap(GameObject player)
    {
        player.GetComponent<PlayerController>().speed /= 2;

        yield return new WaitForSeconds(2f);

        player.GetComponent<PlayerController>().speed *= 2;

        coSap = null;
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3 moveVec;

    private void Update()
    {
        moveVec = Vector3.up * speed * Time.deltaTime;
        transform.position += new Vector3(0, moveVec.y, 0);
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instance.stageManager.player.GetComponent<PlayerController>().Dead();
        }
    }
}

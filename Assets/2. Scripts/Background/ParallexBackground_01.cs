﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParallexBackground_01 : MonoBehaviour
{
    public enum BackgroundType
    {
        back,
        middle,
        front,
    }

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollAmount;
    [SerializeField]
    private Vector3 moveDir;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private BackgroundType backgroundType;

    Vector3 moveVec = new Vector3();
    float moveSpeed;

    private void FixedUpdate()
    {
        switch(backgroundType)
        {
            case BackgroundType.back:
                moveSpeed = player.backgroundSpeed;
                break;
            case BackgroundType.middle:
                moveSpeed = player.backgroundSpeed * 1.2f;
                break;
            case BackgroundType.front:
                moveSpeed = player.backgroundSpeed * 1.5f;
                break;
        }
        moveVec = moveDir * moveSpeed * Time.deltaTime;
        transform.position += moveVec;

        if (transform.position.x - player.gameObject.transform.position.x > 25)
        {
            transform.position = new Vector3(target.transform.position.x - scrollAmount, target.transform.position.y, target.transform.position.z);
        }
        else if (transform.position.x - player.gameObject.transform.position.x < -25)
        {
            transform.position = new Vector3(target.transform.position.x + scrollAmount, target.transform.position.y, target.transform.position.z);
        }
    }
}
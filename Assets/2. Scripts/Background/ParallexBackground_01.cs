using System.Collections;
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

    private Vector3 lastPlayerPosition; // 이전 플레이어 위치 저장
    private float moveSpeed;

    private void Start()
    {
        lastPlayerPosition = player.transform.position; // 초기화
    }

    private void FixedUpdate()
    {
        Vector3 deltaPosition = player.transform.position - lastPlayerPosition; // 플레이어 위치 변화 계산
        lastPlayerPosition = player.transform.position; // 이전 플레이어 위치 업데이트

        switch (backgroundType)
        {
            case BackgroundType.back:
                moveSpeed = deltaPosition.x / 4;
                break;
            case BackgroundType.middle:
                moveSpeed = deltaPosition.x / 2;
                break;
            case BackgroundType.front:
                moveSpeed = deltaPosition.x;
                break;
        }

        Vector3 moveVec = moveSpeed * Time.deltaTime * moveDir;
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

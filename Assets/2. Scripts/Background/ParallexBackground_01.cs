using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParallexBackground_01 : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollAmount;
    [SerializeField]
    private Vector3 moveDir;
    [SerializeField]
    private PlayerController player;

    Vector3 moveVec = new Vector3();

    private void FixedUpdate()
    {
        moveVec = moveDir * player.backgroundSpeed * Time.deltaTime;
        transform.position += moveVec;

        if (transform.position.x - player.gameObject.transform.position.x > 30)
        {
            transform.position = new Vector3(target.transform.position.x - scrollAmount, target.transform.position.y, target.transform.position.z);
        }
        else if (transform.position.x - player.gameObject.transform.position.x < -30)
        {
            transform.position = new Vector3(target.transform.position.x + scrollAmount, target.transform.position.y, target.transform.position.z);
        }
    }
}

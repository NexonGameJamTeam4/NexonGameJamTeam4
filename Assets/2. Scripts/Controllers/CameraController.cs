using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject background;
    public GameObject background2;
    public GameObject background3;
    public GameObject background4;
    public GameObject background5;
    public GameObject background6;

    private Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(0, 0, -10);
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
        background.transform.position = new Vector3(background.transform.position.x, player.transform.position.y, 1);
        background2.transform.position = new Vector3(background2.transform.position.x, player.transform.position.y, 1);
    }
}

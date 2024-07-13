using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public List<GameObject> backgrounds;

    private Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(0, 0, -10);
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
        
        for(int i=0; i<2; i++)
            backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x, player.transform.position.y, 1);
    }
}

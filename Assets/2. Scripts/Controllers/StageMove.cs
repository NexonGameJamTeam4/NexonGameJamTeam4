using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("1");
            if (SceneManager.GetActiveScene().name == "Stage1")
                SceneManager.LoadScene("Stage2");
            else if (SceneManager.GetActiveScene().name == "Stage2")
                SceneManager.LoadScene("Stage3");
        }
    }
}

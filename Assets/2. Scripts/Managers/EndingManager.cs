﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Illersts = new GameObject[4];
    private int page = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextPage();
        }
    }

    private void NextPage()
    {
        if (page == 3)
        {
            SceneManager.LoadScene("Lobby");
            return;
        }

        Illersts[page].SetActive(false);
        Illersts[++page].SetActive(true);
    }
}

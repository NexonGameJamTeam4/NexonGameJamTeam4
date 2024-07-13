using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject pauseMenu;
    public Slider progressSlider;

    public GameObject endBlock;
    public GameObject startBlock;

    float endSize;
    float nowProgress;
    bool isPaused;

    private void Awake()
    {
        instance = this;
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
            return;

        }

        Progress();
    }

    public void Defeat()
    {
        //TODO : 패배 UI 띄우기

        //임시
        SceneManager.LoadScene("Battle");
    }

    public void Progress()
    {
        endSize = Vector2.Distance(endBlock.transform.position, startBlock.transform.position);
        nowProgress = player.transform.position.x / endSize;
        progressSlider.value = nowProgress;
    }
}
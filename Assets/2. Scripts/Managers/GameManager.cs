using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PoolManager poolManager;

    bool isPaused;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
            Time.timeScale = 0;
            isPaused = true;
            return;
            // TODO: UI 보여주기
        }
    }

    public void Defeat()
    {
        //TODO : 패배 UI 띄우기

        //임시
        SceneManager.LoadScene("Battle");
    }
}
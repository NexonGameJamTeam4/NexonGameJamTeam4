using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{

    public GameObject player;

    [Header("# UI")]
    public GameObject pauseMenu;
    public GameObject DefeatMenu;
    public GameObject VictoryMenu;
    public Slider progressSlider;

    [Header("# Block")]
    public GameObject endBlock;
    public GameObject startBlock;

    float endSize;
    float nowProgress;

    public void Victory()
    {
        VictoryMenu.SetActive(true);
        GameManager.instance.isCleared = true;
    }

    public void Defeat()
    {
        //TODO : 패배 UI 띄우기
        StartCoroutine(CoDefeat());
        //임시
        //SceneManager.LoadScene("Stage1");
    }

    IEnumerator CoDefeat()
    {
        yield return new WaitForSeconds(2f);
        DefeatMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Progress()
    {
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            endSize = Vector2.Distance(endBlock.transform.position, startBlock.transform.position);
            nowProgress = player.transform.position.x / endSize;
            progressSlider.value = nowProgress;
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            endSize = Vector2.Distance(endBlock.transform.position, startBlock.transform.position);
            nowProgress = player.transform.position.y / endSize;
            progressSlider.value = nowProgress;
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            endSize = Vector2.Distance(endBlock.transform.position, startBlock.transform.position);
            nowProgress = player.transform.position.x / endSize;
            progressSlider.value = nowProgress;
        }
    }
    public void GoToMain()
    {
        Time.timeScale = 1;

        if (!GameManager.instance.isCleared)
            SceneManager.LoadScene("Lobby");
        else
        {
            GameManager.instance.isCleared = false;
            SceneManager.LoadScene("Ending");
        }
            
    }
}

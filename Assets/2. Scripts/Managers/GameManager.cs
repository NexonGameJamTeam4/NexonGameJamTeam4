using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance = null;

    public static GameManager instance
    {
        get
        {
            if (null == Instance)
            {
                return null;
            }
            return Instance;
        }
    }

    public GameObject player;
    public float clearTime;

    [Header("# UI")]
    public GameObject pauseMenu;
    public GameObject DefeatMenu;
    public GameObject VictoryMenu;
    public Slider progressSlider;

    [Header("# Block")]
    public GameObject endBlock;
    public GameObject startBlock;
    public float gameTime;

    float endSize;
    float nowProgress;
    bool isPaused;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        isPaused = false;
    }

    private void Start()
    {
        clearTime = 0f;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

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

    public void Victory()
    {
        VictoryMenu.SetActive(true);
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
        else if(SceneManager.GetActiveScene().name == "Stage2")
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
}
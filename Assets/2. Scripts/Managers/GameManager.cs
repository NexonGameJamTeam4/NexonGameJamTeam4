using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    float endSize;
    float nowProgress;
    bool isPaused;
    StackedProgress stackedValue;

    private void Awake()
    {
        stackedValue = new StackedProgress();
        instance = this;
        isPaused = false;
    }

    private void Start()
    {
        clearTime = 0f;
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
        SceneManager.LoadScene("Stage1");
    }

    public void Progress()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Stage1"))
        {
            endSize = Vector2.Distance(endBlock.transform.position, startBlock.transform.position);
            nowProgress = player.transform.position.x / endSize;
            progressSlider.value = nowProgress;
        }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Stage2"))
        {
            endSize = Vector2.Distance(endBlock.transform.position, startBlock.transform.position);
            nowProgress = player.transform.position.y / endSize + stackedValue.stackedValue;
            progressSlider.value = nowProgress;
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Stage3"))
        {
            endSize = Vector2.Distance(endBlock.transform.position, startBlock.transform.position);
            nowProgress = player.transform.position.x / endSize + stackedValue.stackedValue;
            progressSlider.value = nowProgress;
        }
    }
}
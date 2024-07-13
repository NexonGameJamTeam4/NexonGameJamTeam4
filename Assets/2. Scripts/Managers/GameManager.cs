using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float clearTime;
    public StageManager stageManager;
    private bool isPaused;
    public bool isCleared;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        isPaused = false;
    }

    private void Start()
    {
        clearTime = 0f;
    }

    private void Update()
    {
        clearTime += Time.deltaTime;

        if (stageManager == null)
            stageManager = FindObjectOfType<StageManager>();

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            stageManager.pauseMenu.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
            Time.timeScale = 0;
            stageManager.pauseMenu.SetActive(true);
            isPaused = true;
            return;

        }

        if (stageManager != null)
            stageManager.Progress();
    }
}
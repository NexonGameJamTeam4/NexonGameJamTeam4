using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.Play("MainBGM", Sound.BGM);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Stage1");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Stage3");
        }
    }

    public void StartBtnClicked()
    {
        // TODO: 스테이지1 첫블럭에서 발 떼면 0초로 초기화
        GameManager.instance.clearTime = 0f;
        SceneManager.LoadScene("Opening");
    }

    public void QuitBtnClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.Play("MainBGM", Sound.BGM);
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

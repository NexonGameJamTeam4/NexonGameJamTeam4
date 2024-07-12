using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.Play("MainBGM", Sound.BGM);
    }

    public void StartBtnClicked()
    {
        Debug.Log("StartBtnClicked");
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

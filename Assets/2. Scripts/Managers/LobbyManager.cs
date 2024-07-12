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

    public void RankBtnClicked()
    {
        Debug.Log("RankBtnClicked");
    }

    public void QuitBtnClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}

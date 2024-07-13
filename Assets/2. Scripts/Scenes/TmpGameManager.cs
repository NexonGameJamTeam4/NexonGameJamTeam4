using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpGameManager : MonoBehaviour
{
    public float clearTime = 0f;

    public void LoadMain()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}

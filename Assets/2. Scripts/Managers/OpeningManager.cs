using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Illersts = new GameObject[4];
    private int page = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            page = 3;
            NextPage();
        }
        else if (Input.anyKeyDown)
        {
            NextPage();
        }
    }

    private void NextPage()
    {
        if (page == 3)
        {
            SceneManager.LoadScene("Stage1");
            return;
        }

        Illersts[page].SetActive(false);
        Illersts[++page].SetActive(true);
    }
}

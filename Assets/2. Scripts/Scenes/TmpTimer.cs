using TMPro;
using UnityEngine;

public class TmpTimer : MonoBehaviour
{
    [SerializeField] private TmpGameManager gameManager;
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private GameObject registerPanel;
    private float passingTime = 0f;
    private bool isPlaying = true;

    private void Update()
    {
        if (!isPlaying)
            return;

        passingTime += Time.deltaTime;
        timeTxt.text = passingTime.ToString();
    }

    public void StopTimer()
    {
        isPlaying = false;
        gameManager.clearTime = passingTime;
        registerPanel.SetActive(true);
    }
}

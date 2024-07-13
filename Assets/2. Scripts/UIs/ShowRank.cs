using BackEnd;
using System.Text;
using TMPro;
using UnityEngine;

public class ShowRank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] rankTxt = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI[] nicknameTxt = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI[] clearTimeTxt = new TextMeshProUGUI[10];

    public void OnGameOver(float score)
    {
        Debug.Log("Your Score:" + score);
        // currentScore = score;
        // scoreText.text = $"Your Score: {currentScore}";
        FetchMyCurrentRank(score);
    }

    void FetchMyCurrentRank(float score)
    {
        // 점수를 제출하고 해당 점수에 대한 랭크를 가져오는 함수
        string rankUUID = "a9a8c7e0-4065-11ef-8ef5-fbf3130cca85";
        var bro = Backend.URank.User.GetRankList(rankUUID, 10);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            if (score.ToString() == jsonData["score"].ToString())
            {
                Debug.Log("랭킹:"+ jsonData["rank"].ToString());
            }
        }
    }

    public void FetchRanking()
    {
        string rankUUID = "a9a8c7e0-4065-11ef-8ef5-fbf3130cca85";
        var bro = Backend.URank.User.GetRankList(rankUUID, 10);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

        int i = 0;
        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            StringBuilder info = new StringBuilder();

            rankTxt[i].text = jsonData["rank"].ToString();
            nicknameTxt[i].text = jsonData["nickname"].ToString();
            clearTimeTxt[i].text = jsonData["score"].ToString();

            i++;
        }
    }
}

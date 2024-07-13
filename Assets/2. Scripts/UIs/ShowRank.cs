using BackEnd;
using System.Text;
using TMPro;
using UnityEngine;

public class ShowRank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] rankTxt = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI[] nicknameTxt = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI[] clearTimeTxt = new TextMeshProUGUI[10];

    [SerializeField] private TextMeshProUGUI myRankTxt;
    [SerializeField] private TextMeshProUGUI myNicknameTxt;
    [SerializeField] private TextMeshProUGUI myClearTimeTxt;

    public void OnGameOver(float score)
    {
        Debug.Log("Your Score:" + score);
        // currentScore = score;
        // scoreText.text = $"Your Score: {currentScore}";
        // FetchMyCurrentRank(score);
    }

    public void FetchMyCurrentRank()
    {
        // 현재 사용자의 랭킹 정보를 가져옴
        string rankUUID = "a9a8c7e0-4065-11ef-8ef5-fbf3130cca85";
        BackendReturnObject bro = Backend.URank.User.GetMyRank(rankUUID);

        if (bro.IsSuccess())
        {
            // 랭킹 정보 파싱
            LitJson.JsonData jsonData = bro.GetFlattenJSON();
            if (jsonData["rows"].Count > 0)
            {
                var rankInfo = jsonData["rows"][0];
                myRankTxt.text = rankInfo["rank"].ToString();
                myNicknameTxt.text = rankInfo["nickname"].ToString();

                float clearTime = float.Parse(rankInfo["score"].ToString());
                // string score = clearTime.ToString("F3");
                myClearTimeTxt.text = clearTime.ToString("F3");

                // 결과를 TextMeshProUGUI에 표시
                // rankingText.text = $"Rank: {rank}\nNickname: {nickname}\nScore: {formattedScore}";
                // Debug.Log($"Rank: {rank}\nNickname: {nickname}\nScore: {score}");
            }
            else
            {
                // rankingText.text = "No ranking data found.";
            }
        }
        else
        {
            Debug.LogError("Failed to fetch user ranking: " + bro.GetErrorCode());
            // rankingText.text = "Failed to fetch ranking.";
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
            float clearTime = float.Parse(jsonData["score"].ToString());
            clearTimeTxt[i].text = clearTime.ToString("F3");

            i++;
        }
    }
}

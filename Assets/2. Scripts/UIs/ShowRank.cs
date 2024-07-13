using BackEnd;
using System.Text;
using TMPro;
using UnityEngine;

public class ShowRank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] rankTxt = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI[] nicknameTxt = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI[] clearTimeTxt = new TextMeshProUGUI[10];

    public void FetchRanking()
    {
        string rankUUID = "a9a8c7e0-4065-11ef-8ef5-fbf3130cca85";
        var bro = Backend.URank.User.GetRankList(rankUUID, 10);

        if (!bro.IsSuccess())
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

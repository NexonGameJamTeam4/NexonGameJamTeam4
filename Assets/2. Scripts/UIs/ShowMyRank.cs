using BackEnd;
using TMPro;
using UnityEngine;

public class ShowMyRank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myRankTxt;
    [SerializeField] private TextMeshProUGUI myNicknameTxt;
    [SerializeField] private TextMeshProUGUI myClearTimeTxt;

    public void FetchMyRank()
    {
        string rankUUID = "a9a8c7e0-4065-11ef-8ef5-fbf3130cca85";
        var bro = Backend.URank.User.GetMyRank(rankUUID);

        if (!bro.IsSuccess())
        {
            Debug.LogError("내 랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("내 랭킹 조회에 성공했습니다. : " + bro);

        // 랭킹 정보 파싱
        LitJson.JsonData jsonData = bro.GetFlattenJSON();

        if (jsonData["rows"].Count == 0)
        {
            Debug.Log("No ranking data found.");
        }

        var rankInfo = jsonData["rows"][0];

        myRankTxt.text = rankInfo["rank"].ToString();
        myNicknameTxt.text = rankInfo["nickname"].ToString();
        float clearTime = float.Parse(rankInfo["score"].ToString());
        myClearTimeTxt.text = clearTime.ToString("F3");
    }
}

using BackEnd;
using TMPro;
using UnityEngine;

public class AutoLogin : MonoBehaviour
{
    [SerializeField] private TmpGameManager gameManager;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private ShowRank showRank;

    [SerializeField] private TMP_InputField nicknameInputField; // 닉네임을 입력받을 TMP_InputField
    [SerializeField] private TextMeshProUGUI resultTxt;        // 결과 메시지를 표시할 TextMeshProUGUI
    private readonly int maxRetryCount = 5;      // 아이디 생성 재시도 최대 횟수

    // 버튼 클릭 시 호출되는 함수
    public void OnRegisterAndLoginButtonClicked()
    {
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            resultTxt.text = "Please enter a nickname.";
            return;
        }

        TryRegisterAndLogin(nickname, maxRetryCount);
    }

    // 랜덤 문자열을 생성하는 함수
    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new System.Random();
        var randomString = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomString[i] = chars[random.Next(chars.Length)];
        }

        return new string(randomString);
    }

    // 회원가입 및 로그인 시도 함수
    private void TryRegisterAndLogin(string nickname, int retryCount)
    {
        if (retryCount <= 0)
        {
            resultTxt.text = "Failed to generate unique ID. Please try again.";
            return;
        }

        string userId = GenerateRandomString(10);
        string password = GenerateRandomString(10);

        // 회원가입
        var bro = Backend.BMember.CustomSignUp(userId, password);

        if (bro.IsSuccess())
        {
            // 회원가입 성공 시 자동 로그인
            var loginBro = Backend.BMember.CustomLogin(userId, password);
            BackendLogin.Instance.UpdateNickname(nickname);

            if (loginBro.IsSuccess())
            {
                Debug.Log($"Login successful!\nUser ID: {userId}\nPassword: {password}\nNickname: {nickname}");
                resultTxt.text = $"Login successful!\nUser ID: {userId}\nPassword: {password}\nNickname: {nickname}";

                // 점수 업데이트
                BackendGameData.Instance.GameDataInsert(gameManager.clearTime);
                BackendRank.Instance.RankInsert(gameManager.clearTime);
                // BackendGameData.Instance.NewRecord(gameManager.clearTime);
                // BackendGameData.Instance.GameDataUpdate();

                // 랭킹 보여주기
                rankingPanel.SetActive(true);
                BackendRank.Instance.RankGet();
                showRank.FetchRanking();

                // 내 랭크와 점수 바로보기
                showRank.FetchMyCurrentRank();
            }
            else
            {
                resultTxt.text = "Login failed: " + loginBro.GetMessage();
                Debug.LogError("Login failed: " + loginBro.GetErrorCode());
            }
        }
        else
        {
            // 아이디 중복으로 회원가입 실패 시 재시도
            if (bro.GetStatusCode() == "409") // 409: Conflict (아이디 중복)
            {
                Debug.LogWarning("ID already exists. Retrying...");
                TryRegisterAndLogin(nickname, retryCount - 1);
            }
            else
            {
                resultTxt.text = "Registration failed: " + bro.GetMessage();
                Debug.LogError("Registration failed: " + bro.GetErrorCode());
            }
        }
    }
}
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// 뒤끝 SDK namespace 추가
using BackEnd;

public class UserData
{
    public float clearTime = 100f;

    // 데이터를 디버깅하기 위한 함수입니다.(Debug.Log(UserData);)
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"clearTime : {clearTime}");

        return result.ToString();
    }
}

public class BackendGameData
{
    private static BackendGameData _instance = null;

    public static BackendGameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameData();
            }

            return _instance;
        }
    }

    public static UserData userData;

    private string gameDataRowInDate = string.Empty;

    public void GameDataInsert()
    {
        if (userData == null)
        {
            userData = new UserData();
            Debug.Log("데이터를 초기화합니다.");
            userData.clearTime = 100f;

            Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가합니다.");
            Param param = new Param();
            param.Add("clearTime", userData.clearTime);


            Debug.Log("게임 정보 데이터 삽입을 요청합니다.");
            var bro = Backend.GameData.Insert("USER_DATA", param);

            if (bro.IsSuccess())
            {
                Debug.Log("게임 정보 데이터 삽입에 성공했습니다. : " + bro);

                //삽입한 게임 정보의 고유값입니다.  
                gameDataRowInDate = bro.GetInDate();
            }
            else
            {
                Debug.LogError("게임 정보 데이터 삽입에 실패했습니다. : " + bro);
            }
        }
    }

    public void GameDataGet()
    {
        // Step 3. 게임 정보 불러오기 구현하기
    }

    public void LevelUp()
    {
        // Step 4. 게임 정보 수정 구현하기
    }

    public void GameDataUpdate()
    {
        // Step 4. 게임 정보 수정 구현하기
    }
}
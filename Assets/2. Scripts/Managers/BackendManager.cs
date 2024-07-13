using BackEnd;
using System.Threading.Tasks;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    void Start()
    {
        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }

        Test();
    }

    async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234");

            Debug.Log("테스트를 종료합니다.");
        });
    }
}

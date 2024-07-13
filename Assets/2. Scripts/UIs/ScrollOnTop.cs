using UnityEngine;
using UnityEngine.UI;

public class ScrollOnTop : MonoBehaviour
{
    public ScrollRect scrollRect;

    void Start()
    {
        // 스크롤 뷰를 가장 위로 설정
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
}

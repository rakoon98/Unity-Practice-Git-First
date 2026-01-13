using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    public string shopName = "강화 상점";

    // 플레이어가 말을 걸었을 때 호출될 함수
    public void OpenShop()
    {
        Debug.Log($"{shopName}에 오신 것을 환영합니다! 무엇을 도와드릴까요?");
        // (나중에 12단계에서 여기에 실제 UI를 띄우는 로직이 들어갑니다)
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopNPC : MonoBehaviour
{
    public string shopName = "강화 상점";

    // 플레이어가 말을 걸었을 때 호출될 함수
    public void OpenShop()
    {
        Debug.Log($"{shopName}에 오신 것을 환영합니다! 무엇을 도와드릴까요?");
        // (나중에 12단계에서 여기에 실제 UI를 띄우는 로직이 들어갑니다)
    }

    // 임시 테스트용 (update 에서 1번 키 감지)
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     // 주변 플레이어를 찾아 강화 적용
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     if(player.TryGetComponent<StatHandler>(out StatHandler stat))
        //     {
        //         stat.EnhanceSpeed(0.2f);
        //     }
        // }

        // 새로운 방식 (간단 테스트용):
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("숫자 1 키를 눌렀습니다! (New Input System 방식)");
            // 주변 플레이어를 찾아 강화 적용
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player == null)
            {
                Debug.Log($"[ShopNPC] player not found");                
                return;
            }
            
            if(player.TryGetComponent<StatHandler>(out StatHandler stat))
            {
                stat.EnhanceSpeed(0.2f);
            } else
            {
                Debug.Log($"StatHandler not found");
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E 키를 눌렀습니다! (상점 열기 테스트)");
        }
    }
}
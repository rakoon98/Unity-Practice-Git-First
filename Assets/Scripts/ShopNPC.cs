using UnityEngine;
using UnityEngine.InputSystem;

public class ShopNPC : MonoBehaviour
{
    public string shopName = "강화 상점";

    [Header("판매 아이템")]
    public GameObject fireSkillPrefab;

    private PlayerController currentCustomer; // 나에게 말을 걸 플레이어 저장

    // 플레이어가 말을 걸었을 때 호출될 함수
    public void OpenShop()
    {
        Debug.Log($"{shopName}에 오신 것을 환영합니다! 무엇을 도와드릴까요?");
        // (나중에 12단계에서 여기에 실제 UI를 띄우는 로직이 들어갑니다)
        Debug.Log($"{shopName}: 2번 키를 눌러 스킬을 구매하세요.");
        

        if (currentCustomer != null)
        {
            // 드디어 호출! 플레이어를 '대화 중' 상태로 만듭니다.
            currentCustomer.SetInteracting(true);
            Debug.Log($"{shopName} 오픈! (플레이어 이동 정지)");
        }
    }

    public void CloseShop()
    {
        if (currentCustomer != null)
        {
            currentCustomer.SetInteracting(false); // 다시 움직일 수 있게 함
            Debug.Log("상점을 닫습니다. 안녕히 가세요!");
        }
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
        } else if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Debug.Log($"스킬구매하기 버튼2클릭");
            BuySkill();
        }


        // 테스트용: ESC 키를 누르면 상점 닫기      
        // Update 에서 하는게 아니라 이것도 키인풋 설정해서 하는게 맞음.  
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseShop();
        }


        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E 키를 눌렀습니다! (상점 열기 테스트)");
        }
    }

    public void BuySkill()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null && player.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            if (fireSkillPrefab != null)
            {
                pc.EquipSkill(fireSkillPrefab); // 플레이어에게 프리팹 전달
                Debug.Log($"{fireSkillPrefab.name} 구매 완료!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 07단계에서 배운 TryGetComponent 활용
        if (other.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            currentCustomer = pc;
            Debug.Log("손님 입장! 이제 대화가 가능합니다.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 상점 범위를 나가면 조작 금지를 풀어주고 참조 제거
            if (currentCustomer != null)
            {
                currentCustomer.SetInteracting(false);
                currentCustomer = null;
            }
        }
    }
}
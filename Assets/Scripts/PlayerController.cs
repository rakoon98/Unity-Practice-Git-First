using UnityEngine;
using UnityEngine.InputSystem; // 새 입력 시스템 사용

public class PlayerController : MonoBehaviour
{

    public CharacterData stat;
    private Vector2 moveInput;  
    
    public float moveSpeed = 5f;

    private ShopNPC nearbyNPC; // 현재 근처에 있는 NPC 저장
    
    void Start()
    {
        
    }

    void Update()
    {        
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        
        if (moveDir != Vector3.zero)
            transform.forward = moveDir;
    }

    // 05단계에서 만든 트리거 로직을 활용
    void OnTriggerEnter(Collider other)
    {
        // 07단계 핵심: 컴포넌트 통신 (대상에게 ShopNPC 컴포넌트가 있는지 확인)
        if (other.TryGetComponent<ShopNPC>(out ShopNPC npc))
        {
            nearbyNPC = npc;
            Debug.Log($"{npc.shopName} 범위 진입. (E키를 누르세요)");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            nearbyNPC = null;
            Debug.Log("상점 범위를 벗어남.");
        }
    }

    // 1. Move 액션 메시지 수신 (Input System이 자동 호출)
    public void OnMove(InputValue value)
    {
        Debug.Log("움직임!");
        moveInput = value.Get<Vector2>();
    }

    // 2. Attack 액션 메시지 수신
    public void OnAttack()
    {
        Debug.Log("공격 수행!");
        // 여기서 05단계의 AttackRange 트리거를 활성화하거나 애니메이션을 실행합니다.
    }

    // 3. Interact 액션 메시지 수신
    public void OnInteract()
    {
        Debug.Log("상호작용 버튼(E) 눌림!");

        if (nearbyNPC != null)
        {
            nearbyNPC.OpenShop(); // NPC의 함수를 직접 호출!
        }
        else
        {
            Debug.Log("상호작용할 대상이 없습니다.");
        }
    }
    
}

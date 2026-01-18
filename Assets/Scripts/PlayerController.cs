using UnityEngine;
using UnityEngine.InputSystem; // 새 입력 시스템 사용

public class PlayerController : MonoBehaviour
{

    public CharacterData stat;
    private Vector2 moveInput;

    public float moveSpeed = 5f;

    private ShopNPC nearbyNPC; // 현재 근처에 있는 NPC 저장

    public GameObject skillPrefab; // 상점에서 구매 시 여기에 할당됨.
    public Transform firePoint; // 발사 위치(플레이어 앞)

    public enum PlayerState 
    { 
        Idle,       // 아무것도 안 함
        Moving,     // 이동 중
        Attacking,  // 공격 애니메이션 중 (이동 불가)
        Interacting // NPC와 대화 중 (조작 불가)
    }
    public PlayerState currentState = PlayerState.Idle; 


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
        // 대화 중이거나 공격 중일 때는 이동 입력을 무시함
        if (currentState == PlayerState.Interacting || currentState == PlayerState.Attacking)
        {
            moveInput = Vector2.zero;
            return;
        }

        Debug.Log("움직임!");
        moveInput = value.Get<Vector2>();

        // 이동 입력이 있으면 상태 변경
        if (moveInput.magnitude > 0) currentState = PlayerState.Moving;
        else currentState = PlayerState.Idle;
    }

    // 2. Attack 액션 메시지 수신
    public void OnAttack()
    {
        // 이미 공격 중이거나 대화 중이면 공격 불가
        if (currentState == PlayerState.Attacking || currentState == PlayerState.Interacting) 
            return;
    
        // 공격 상태로 전환
        currentState = PlayerState.Attacking;

        if (skillPrefab != null)
        {
            Instantiate(skillPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("공격! (공격 중에는 이동 불가)");
        }

        // 0.5초 후에 다시 Idle 상태로 복구 (임시 코루틴)
        Invoke("ResetState", 0.5f);
        

        // // 09. 스킬사용
        // if (skillPrefab != null)
        // {
        //     Instantiate(skillPrefab, firePoint.position, firePoint.rotation);
        //     Debug.Log("파이어볼!");
        // }
        // else
        // {
        //     Debug.Log("사용 가능한 스킬이 없습니다.");
        // }
    }

    private void ResetState()
    {
        currentState = PlayerState.Idle;
    }

    public void SetInteracting(bool isInteracting)
    {
        if (isInteracting) currentState = PlayerState.Interacting;
        else currentState = PlayerState.Idle;
    }

    // 3. Interact 액션 메시지 수신
    public void OnInteract()
    {
        currentState = PlayerState.Interacting;
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

    public void EquipSkill(GameObject newSkill)
    {
        skillPrefab = newSkill;
        Debug.Log($"{newSkill.name} 스킬을 장착했습니다!");
    }

}

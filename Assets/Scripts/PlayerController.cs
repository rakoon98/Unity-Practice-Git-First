using UnityEngine;
using UnityEngine.InputSystem; // 새 입력 시스템 사용

public class PlayerController : MonoBehaviour
{

    public CharacterData stat;
    private Vector2 moveInput;  
    
    public float moveSpeed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        // // 1. 키보드 입력 받기 (WASD 혹은 방향키)
        // float h = Input.GetAxisRaw("Horizontal"); // A, D
        // float v = Input.GetAxisRaw("Vertical");   // W, S

        // // 2. 이동 방향 계산
        // Vector3 moveDir = new Vector3(h, 0, v).normalized;

        // // 3. 실제 이동 처리
        // transform.position += moveDir * moveSpeed * Time.deltaTime;

        // // 4. 이동 방향 바라보기 (보너스: 캐릭터 회전)
        // if (moveDir != Vector3.zero)
        // {
        //     transform.forward = moveDir;
        // }
        
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        
        if (moveDir != Vector3.zero)
            transform.forward = moveDir;
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
    }
}

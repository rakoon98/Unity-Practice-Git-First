using UnityEngine;

public class StatHandler : MonoBehaviour
{

    // 03단계에서 만든 캐릭터 데이터.   
    [SerializeField] private CharacterData baseData;

    // 적용 실제 데이터
    public float currentMoveSpeed;
    public int currentAttackPower;

    // 강화 수치 기록 변수
    private float speedMultiplier = 1.0f; // 1.0 == 100%

    void Start()
    {
        // 초기화: 데이터 에셋의 기본값 가져오기
        UpdateFinalStats();
    }

    void Update()
    {
        
    }

    // 강화 로직: 이동 속도를 특정 비율만큼 증가
    public void EnhanceSpeed(float percentage)
    {
        // ex. 20% 증가 시 percentage는 0.2f
        speedMultiplier += percentage;
        UpdateFinalStats();
        Debug.Log($"강화 성공! 현재 이동 속도 배율: {speedMultiplier * 100}%");
    }

    private void UpdateFinalStats()
    {
        // 최종 속도 = 기본 속도 * 배율;
        currentMoveSpeed = baseData.moveSpeed * speedMultiplier;

        // 실제 플레이어 컨트롤러에 속도 전달
        if(TryGetComponent<PlayerController>(out PlayerController pc))
        {
            Debug.Log($"속도 적용 pc.moveSpeed = currentMoveSpeed [{currentMoveSpeed}]");
            pc.moveSpeed = currentMoveSpeed;
        } else
        {
            Debug.Log($"has not playerContoller");
        }
    }
}

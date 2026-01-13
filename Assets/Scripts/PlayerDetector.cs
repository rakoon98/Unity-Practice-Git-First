using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    // 트리거 범위 안에 무언가 들어왔을 때 실행
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Debug.Log("상점 NPC 근처에 도착! (E키로 상호작용 가능)");
        }
        
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적 발견! 공격 범위 내에 들어옴.");
        }
    }

    // 트리거 범위 밖으로 나갔을 때 실행
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Debug.Log("상점 영역을 벗어남.");
        }
    }
}
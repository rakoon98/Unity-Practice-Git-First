using UnityEngine;

// 게임의 상태를 정의하는 열거형
public enum GameState { Battle, Shop }

public class GameManager : MonoBehaviour
{
    public GameState currentState; // 현재 상태
    public float phaseTimer = 10f; // 각 페이즈당 시간 (10초)
    private float timer;

    void Start()
    {
        timer = phaseTimer;
        currentState = GameState.Battle; // 시작은 전투 단계
        Debug.Log("전투 단계 시작!");
    }

    void Update()
    {
        // 1. 타이머 작동
        timer -= Time.deltaTime;

        // 2. 시간이 다 되면 상태 전환
        if (timer <= 0)
        {
            SwitchPhase();
        }
    }

    void SwitchPhase()
    {
        timer = phaseTimer; // 타이머 초기화

        if (currentState == GameState.Battle)
        {
            currentState = GameState.Shop;
            Debug.Log("상점 단계로 전환되었습니다!");
        }
        else
        {
            currentState = GameState.Battle;
            Debug.Log("전투 단계로 전환되었습니다!");
        }
    }
}
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        // 3초 뒤에 자동으로 메모리에서 삭제 (최적화)
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 앞방향으로 지속 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
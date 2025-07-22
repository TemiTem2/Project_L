using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float hp = 10f;
    public Player player;

    private void Start()
    {
        player = FindFirstObjectByType<Knight>();
    }
    void Update()
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
            player.TakeDamage(1f); // 예시로 Space 키를 누르면 1의 피해를 받음
            }
    }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            Debug.Log("Damage taken: " + damage + ", Remaining HP: " + hp);
        }
 }



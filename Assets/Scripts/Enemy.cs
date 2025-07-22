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
            player.TakeDamage(1f); // ���÷� Space Ű�� ������ 1�� ���ظ� ����
            }
    }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            Debug.Log("Damage taken: " + damage + ", Remaining HP: " + hp);
        }
 }



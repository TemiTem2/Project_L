using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float hp = 10f;

    void Update()
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
            StateManager.TakeDamage(10f); // ���÷� Space Ű�� ������ 10�� ���ظ� ����
            }
    }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            Debug.Log("Damage taken: " + damage + ", Remaining HP: " + hp);
        }
 }



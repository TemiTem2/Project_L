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
            StateManager.TakeDamage(10f); // 예시로 Space 키를 누르면 10의 피해를 받음
            }
    }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            Debug.Log("Damage taken: " + damage + ", Remaining HP: " + hp);
        }
 }



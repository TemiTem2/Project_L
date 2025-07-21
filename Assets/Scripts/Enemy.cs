using UnityEngine;

public class Enemy : MonoBehaviour
{

        public float hp = 10f;

        // Update is called once per frame
        void Update()
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            Debug.Log("Damage taken: " + damage + ", Remaining HP: " + hp);
        }
 }



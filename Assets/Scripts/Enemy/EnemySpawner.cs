using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Vector2 fieldRangeX;
    private Vector2 fieldRangeY;

    private GameObject field;

    private void Start()
    {
        SetFieldRange();
    }


    private void SetFieldRange()
    {
        field = GameObject.Find("Field");
        if (field != null)
        {
            BoxCollider2D fieldCollider = field.GetComponent<BoxCollider2D>();
            if (fieldCollider != null)
            {
                fieldRangeX = new Vector2(fieldCollider.bounds.min.x, fieldCollider.bounds.max.x);
                fieldRangeY = new Vector2(fieldCollider.bounds.min.y, fieldCollider.bounds.max.y);
            }
            else
            {
                Debug.LogWarning("Field에 BoxCollider2D 없음");
            }
        }
        else
        {
            Debug.LogWarning("Field 없음");
        }
    }

    public void SpawnEnemy(string tag)
    {
        Vector2 position = GenerateSpwanPos();
        GameObject enemy = PoolManager.Instance.GetObject(PoolType.Enemy, tag, position, Quaternion.identity);
        if (enemy == null) Debug.LogWarning("풀에서 꺼낼 수 없음: " + tag);
    }

    private Vector2 GenerateSpwanPos()
    {
        int randomDirection = Random.Range(0, 4);
        float posX = GenerateRandomPosX();
        float posY = GenerateRandomPosY();

        switch (randomDirection)
        {
            case 0: // Left
                posX = fieldRangeX.x + 1f;
                break;
            case 1: // Right
                posX = fieldRangeX.y - 1f;
                break;
            case 2: // Up
                posY = fieldRangeY.y - 1f;
                break;
            case 3: // Down
                posY = fieldRangeY.x + 1f;
                break;
        }
                return new Vector2(posX, posY);
    }

    private float GenerateRandomPosX()
    {
        return Random.Range(fieldRangeX.x, fieldRangeX.y);
    }
    private float GenerateRandomPosY()
    {
        return Random.Range(fieldRangeY.x, fieldRangeY.y);
    }
}

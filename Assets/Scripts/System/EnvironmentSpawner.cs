using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] smallObjs;
    [SerializeField] GameObject[] largeObjs;

    [SerializeField] int smallObjCount = 10;
    [SerializeField] int largeObjCount = 4;

    [SerializeField] GameObject field;
    [SerializeField] GameObject Background;
    private Vector3 fieldRangeX;
    private Vector3 fieldRangeY;
    void Start()
    {
        SetFieldRange();
        SpawnEnvironment();
    }

    // Update is called once per frame
    void SetFieldRange()
    {
        BoxCollider2D fieldCollider = field.GetComponent<BoxCollider2D>();
        if (fieldCollider != null)
        {
            fieldRangeX = new Vector3(fieldCollider.bounds.min.x, fieldCollider.bounds.max.x);
            fieldRangeY = new Vector3(fieldCollider.bounds.min.y, fieldCollider.bounds.max.y);
        }
    }

    public void SpawnEnvironment()
    {
        for (int i = 0; i < smallObjCount; i++)
        {
            Vector3 position = GenerateSpwanPos();
            int randomIndex = Random.Range(0, smallObjs.Length);
            GameObject smallobj = Instantiate(smallObjs[randomIndex], position, Quaternion.identity);
            smallobj.transform.SetParent(Background.transform);
        }
        for (int i = 0; i < largeObjCount; i++)
        {
            Vector3 position = GenerateSpwanPos();
            int randomIndex = Random.Range(0, largeObjs.Length);
            GameObject largeobj = Instantiate(largeObjs[randomIndex], position, Quaternion.identity);
            largeobj.transform.SetParent(Background.transform);
        }
    }

    private Vector3 GenerateSpwanPos()
    {
        
        float posX = Random.Range(fieldRangeX.x, fieldRangeX.y);
        float posY = Random.Range(fieldRangeY.x, fieldRangeY.y);

        return new Vector3(posX, posY, 0);
    }
}

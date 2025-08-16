using UnityEngine;

public class TrackingObjecct : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform target;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrowDistance = 50f;

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(target.position);
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            arrow.SetActive(true);
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
            Vector3 direction = (screenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0)).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0) + direction* arrowDistance; 
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            arrow.SetActive(false);
        }
    }
}

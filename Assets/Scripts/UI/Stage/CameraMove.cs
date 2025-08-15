using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = playerTransform.position;
            newPosition.z = -10f; // 카메라의 Z 위치를 고정
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("PlayerMove not found. Camera will not follow.");
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }
}

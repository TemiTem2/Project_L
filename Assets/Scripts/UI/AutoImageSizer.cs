using UnityEngine;
using UnityEngine.UI;

public class AutoImageSizer : MonoBehaviour
{
    void Update()
    {
        Image image = GetComponent<Image>();
        if (image != null && image.sprite != null)
        {
            image.SetNativeSize();
        }
    }
}


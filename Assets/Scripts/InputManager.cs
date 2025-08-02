using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool isClicked = false;

    private void Update()
    {
        isClicked = ClickCheck();
    }
    
    private bool ClickCheck()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
    }
}

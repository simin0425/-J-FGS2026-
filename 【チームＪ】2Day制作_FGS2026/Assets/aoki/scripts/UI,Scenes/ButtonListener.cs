using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonListener : MonoBehaviour
{
    [SerializeField]private SceneChanger.Scene changeScene;
    private void Update()
    {
        if (Gamepad.current == null)
        {
            return;
        }
        if (Gamepad.current.wasUpdatedThisFrame)
        {
            SceneChanger.ChangeScene(changeScene);
        }
    }
}

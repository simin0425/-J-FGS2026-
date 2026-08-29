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
        if (IsAnyButtonTriggerd())
        {
            SceneChanger.ChangeScene(changeScene);
        }
    }

    bool IsAnyButtonTriggerd()
    {
        var gamePad = Gamepad.current;

        if (gamePad.buttonEast.wasPressedThisFrame) return true;
        if (gamePad.buttonSouth.wasPressedThisFrame) return true;
        if (gamePad.buttonNorth.wasPressedThisFrame) return true;
        if (gamePad.buttonWest.wasPressedThisFrame) return true;

        var keyboard = Keyboard.current;
        if (keyboard.enterKey.wasPressedThisFrame) return true;

        return false;
    }
}

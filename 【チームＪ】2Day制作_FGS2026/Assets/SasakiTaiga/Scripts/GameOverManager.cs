using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverManager : MonoBehaviour
{
    bool isSelectingRetry = true;

    [SerializeField] GameObject retry;
    [SerializeField] GameObject back2Title;

    [SerializeField] float selectionFontSize = 65;
    [SerializeField] float unSelectionFontSize = 30;
    [SerializeField] Color selectionColor = Color.white;
    [SerializeField] Color unSelectionColor = Color.gray;

    TextMeshProUGUI retryTMP;
    TextMeshProUGUI back2TitleTMP;

    private void Start()
    {
        retryTMP = retry.GetComponent<TextMeshProUGUI>();
        back2TitleTMP = back2Title.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var stick = Gamepad.current.leftStick.value;

        if (stick.y > 0.5f)
        {
            isSelectingRetry = true;
            retryTMP.fontSize = selectionFontSize;
            retryTMP.color = selectionColor;
            back2TitleTMP.fontSize = unSelectionFontSize;
            back2TitleTMP.color = unSelectionColor;
        }
        else if (stick.y < -0.5f)
        {
            isSelectingRetry = false;
            retryTMP.fontSize = unSelectionFontSize;
            retryTMP.color = unSelectionColor;
            back2TitleTMP.fontSize = selectionFontSize;
            back2TitleTMP.color = selectionColor;
        }

        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (isSelectingRetry)
            {
                SceneChanger.ChangeScene(SceneChanger.Scene.Stage01);
            }
            else
            {
                SceneChanger.ChangeScene(SceneChanger.Scene.TitleScene);
            }
        }
    }

    public bool IsSelectingRetry()
    {
        return isSelectingRetry;
    }
}

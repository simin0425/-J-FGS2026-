using UnityEngine;
using UnityEngine.InputSystem;

public class HP_UI_FR : MonoBehaviour
{

    public int hp = 3;

    void OnGUI()
    {
        GUI.Box(new Rect())
    }











    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
    }
}

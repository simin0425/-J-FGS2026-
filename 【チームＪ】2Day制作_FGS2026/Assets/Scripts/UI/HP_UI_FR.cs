using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HP_UI_FR : MonoBehaviour
{

    public int hp = 3;

   










    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            hp--;
        }
    }
}

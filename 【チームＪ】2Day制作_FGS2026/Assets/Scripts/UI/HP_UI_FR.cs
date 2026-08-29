using UnityEngine;
using UnityEngine.InputSystem;

public class HP_UI_FR : MonoBehaviour
{

    public int hp = 3;
    public GameObject[] hearts; //ハート画像を3つ入れる


void Damage()
    {
        if(hp <= 0) return;

        hp--;
        hearts[hp].SetActive(false);
    }










}

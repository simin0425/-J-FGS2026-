using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;


public class HP_UI_FR : MonoBehaviour
{
    public static HP_UI_FR Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public int hp = 3;
    public GameObject[] hearts;

    public GameObject FadeObject;
     //ハート画像を3つ入れる


public void Damage()
    {
        //0ならなにもしない（マイナス防止）
        if(hp <= 0) return;

        hp--;

        //アニメーションを変更する
        Animator animator = hearts[hp].GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("ShouldBreak", true);
        }
    }

    public void BeGameOver()
    {
          if (hp <= 0)
        {
            if (FadeObject != null)
            {
                FadeManager fadeManager = FadeObject.GetComponent<FadeManager>();
                if (fadeManager != null)
                {
                    fadeManager.FadeOutAndLoad(SceneChanger.Scene.GameOverScene);
                }
            }
            else
            {
                Debug.LogWarning("FadeObject or FadeManager is not assigned.");
                SceneChanger.ChangeScene(SceneChanger.Scene.GameOverScene);
            }
        }
    }


void Update()
    {
        // Wキーが押されたらダメージ
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Damage();
        }
    }

void Start()
    {
        foreach (GameObject heart in hearts)
        {
            UIImage_Base imageBase = heart.GetComponent<UIImage_Base>();
            if (imageBase != null)
            {
                imageBase.SetOwner(this);
            }
        }
    }










}

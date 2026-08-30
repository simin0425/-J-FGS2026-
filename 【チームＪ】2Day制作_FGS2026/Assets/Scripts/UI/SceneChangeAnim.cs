using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class FadeManager : MonoBehaviour
{
    public Image fadePanel;  // 黒いImageにCanvasGroupを付けたもの
    public float fadeTime = 1f;

    public void FadeOutAndLoad(SceneChanger.Scene scene)
    {
       StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeOut(SceneChanger.Scene scene)
    {
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            
            fadePanel.fillAmount+=0.1f;

            yield return null;
        }

        SceneChanger.ChangeScene(scene);
    }

    void Start()
    {
        // 初期状態で透明にする
        Color color = fadePanel.color;
        color.a = 0f;
        fadePanel.color = color;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public enum Scene
    {
        TitleScene,
        Stage01,
        GameScene,
        ClearScene,
        GameOverScene
    }

    /// <summary>
    /// シーンを変更する
    /// </summary>
    /// <param name="scene">次のシーン</param>
    public static void ChangeScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
using UnityEngine;

public class FallChecker : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float FallHeight = -11.0f;

    private void Update()
    {
        if (player.position.y < FallHeight)
        {
            SceneChanger.ChangeScene(SceneChanger.Scene.GameOverScene);
        }
    }
}

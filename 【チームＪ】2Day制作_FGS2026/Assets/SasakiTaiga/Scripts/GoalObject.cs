using UnityEngine;

public class GoalObject : MonoBehaviour
{
    [SerializeField]
    string PlayerTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            SceneChanger.ChangeScene(SceneChanger.Scene.ClearScene);
        }
    }
}

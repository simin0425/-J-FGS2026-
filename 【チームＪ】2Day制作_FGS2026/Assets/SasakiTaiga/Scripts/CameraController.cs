using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float offsetY = 1.5f;

    [SerializeField]
    GameObject player;
    [SerializeField]
    float lerpT = 5.0f;

    [SerializeField]
    Transform leftBottomLimit;
    [SerializeField]
    Transform rightTopLimit;

    const float kScreenWidth = 9 * 2;
    const float kScreenHeight = 5 * 2;

    private void Start()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y += offsetY;
        playerPos.z = -10.0f;
        this.transform.position = playerPos;

        if (leftBottomLimit == null)
        {
            Debug.LogWarning("CameraのLeftBottomLimitが設定されていません");
        }
        if (rightTopLimit == null)
        {
            Debug.LogWarning("CameraのRightTopLimitが設定されていません");
        }
    }

    void LateUpdate()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y += offsetY;
        playerPos.z = -10.0f;
        Vector3 thisPos = this.transform.position;
        Vector3 afterPos = Vector3.Lerp(thisPos, playerPos, lerpT * Time.deltaTime);

        this.transform.position = afterPos;

        // 画面左下端の移動制限
        thisPos = this.transform.position;
        if (leftBottomLimit != null)
        {
            // 左端
            if (this.transform.position.x < leftBottomLimit.position.x + kScreenWidth / 2)
            {
                thisPos.x = leftBottomLimit.position.x + kScreenWidth / 2;
            }
            // 下端
            if (this.transform.position.y < leftBottomLimit.position.y + kScreenHeight / 2)
            {
                thisPos.y = leftBottomLimit.position.y + kScreenHeight / 2;
            }
        }
        // 画面右上端の移動制限
        if (rightTopLimit != null)
        {
            // 右端
            if (this.transform.position.x > rightTopLimit.position.x - kScreenWidth / 2)
            {
                thisPos.x = rightTopLimit.position.x - kScreenWidth / 2;
            }
            // 上端
            if (this.transform.position.y > rightTopLimit.position.y - kScreenHeight / 2)
            {
                thisPos.y = rightTopLimit.position.y - kScreenHeight / 2;
            }
        }
        this.transform.position = thisPos;
    }
}

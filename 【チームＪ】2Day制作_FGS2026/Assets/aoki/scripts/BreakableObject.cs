using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public void Break()
    {
        //パーティクル
        Destroy(gameObject);
    }
}

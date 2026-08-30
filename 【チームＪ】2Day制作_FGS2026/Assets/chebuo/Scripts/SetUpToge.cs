using Unity.VisualScripting;
using UnityEngine;

public class SetUpToge : MonoBehaviour
{
    Transform[] children;

    void Awake()
    {
        children=new Transform[this.gameObject.transform.childCount];

        int count=0;
        foreach(Transform child in this.transform)
        {
            children[count]=child;
            count++;
            child.AddComponent<BoxCollider2D>();
            
        }
        Debug.Log(children.Length);
    }

}
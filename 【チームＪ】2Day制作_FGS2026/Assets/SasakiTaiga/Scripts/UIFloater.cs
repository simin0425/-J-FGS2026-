using UnityEngine;

public class UIFloater : MonoBehaviour
{
    [SerializeField] float sinRate = 0.0f;
    [SerializeField] float sinScale = 1.0f;

    Vector3 defaultPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultPos = this.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(sinRate * Time.time);
        Vector3 pos = defaultPos;
        pos.y = defaultPos.y + sin * sinScale;
        this.GetComponent<RectTransform>().position = pos;
    }
}

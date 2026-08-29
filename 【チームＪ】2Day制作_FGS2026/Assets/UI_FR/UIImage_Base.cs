using UnityEngine;

public class UIImage_Base : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateImage()
    {
        gameObject.SetActive(false);
    }
}

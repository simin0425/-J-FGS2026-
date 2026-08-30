using UnityEngine;

public class UIImage_Base : MonoBehaviour
{

    private HP_UI_FR Owner_HP_UI_FR;

    public void SetOwner(HP_UI_FR owner)
    {
        Owner_HP_UI_FR = owner;
    }
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

        if (Owner_HP_UI_FR != null)
        {
            Owner_HP_UI_FR.BeGameOver();
        }
    }
}

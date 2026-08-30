using TMPro;
using UnityEngine;

public class UIBrinker : MonoBehaviour
{
    [SerializeField] float brinkRate = 0.5f;
    [SerializeField] GameObject ui;

    private void Update()
    {
        if ((Time.time % brinkRate * 2) > brinkRate)
        {
            ui.SetActive(false);
        }
        else
        {
            ui.SetActive(true);
        }
    }
}

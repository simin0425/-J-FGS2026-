using System;
using System.Collections;
using UnityEngine;

public class SpriteFliper : MonoBehaviour
{
    [SerializeField]private SpriteRenderer mySpriteRenderer;
    [SerializeField]private Sprite[] sprites;
    [SerializeField]private float flipInterval;
    private void Start()
    {
        StartCoroutine(FlipSprits());
    }

    private IEnumerator FlipSprits()
    {
        while (true)
        {
            foreach (var sprite in sprites)
            {
                mySpriteRenderer.sprite = sprite;
                yield return new WaitForSeconds(flipInterval);
            }
        }
    }
}

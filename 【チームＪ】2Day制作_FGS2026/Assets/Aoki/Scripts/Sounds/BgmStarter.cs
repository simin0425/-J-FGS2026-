using UnityEngine;

public class BgmStarter : MonoBehaviour
{
    [SerializeField]private AudioClip bgmClip; 
    void Start()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayBGM(bgmClip);
    }
}

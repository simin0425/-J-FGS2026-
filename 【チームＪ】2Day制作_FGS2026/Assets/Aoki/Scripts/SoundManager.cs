using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource SESource;
    [SerializeField] private AudioSource BGMSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySE(AudioClip seClip, float seVolumeScale = 1f)
    {
        SESource.PlayOneShot(seClip, seVolumeScale);
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        BGMSource.clip = bgmClip;
        BGMSource.Play();
    }
    public void StopSE()
    {
        BGMSource.Stop();
    }



}
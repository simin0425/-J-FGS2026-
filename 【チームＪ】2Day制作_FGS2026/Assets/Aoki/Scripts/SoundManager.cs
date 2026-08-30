using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource SESource;
    [SerializeField] private AudioSource BGMSource;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        LoopBGM();
    }

    private void LoopBGM()
    {
        if (BGMSource.isPlaying)
        {
            return;
        }

        if (BGMSource.clip)
        {
            BGMSource.Play();
        }
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
    public void StopBGM()
    {
        BGMSource.Stop();
        BGMSource.clip = null;
    }



}
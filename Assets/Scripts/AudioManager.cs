using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private void Awake()
    {
        instance = this;

    }
    private int bgmIndex;
    private void Update()
    {
        if (!bgm[bgmIndex].isPlaying)
        {
            PlayRandomBGM();
        }

    }
    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);

    }
    public void PlaySFX(int index)
    {
        if (index < sfx.Length)
        {
            sfx[index].pitch = Random.Range(.85f, 1.1f);
            sfx[index].Play();
        }

    }
    public void StopSFX(int index)
    {
        sfx[index].Stop();
    }
    public void PlayBGM(int index)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[index].Stop();

        }
        bgm[index].Play();

    }
    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }



}

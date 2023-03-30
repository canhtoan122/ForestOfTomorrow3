using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance;
    public List<AudioClip> backgroundMusicList;
    private AudioSource audioSource;
    private int currentMusicIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        currentMusicIndex = 0;
        PlayBackgroundMusic();
        
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusicList.Count > 0)
        {
            audioSource.clip = backgroundMusicList[currentMusicIndex];
            audioSource.Play();
            currentMusicIndex = (currentMusicIndex + 1) % backgroundMusicList.Count;
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayBackgroundMusic();
        }
    }
}

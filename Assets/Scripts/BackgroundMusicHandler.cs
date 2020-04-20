using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the storage and playback of background music.
/// </summary>
public class BackgroundMusicHandler : MonoBehaviour
{
    public AudioSource normalBGM;
    public AudioSource upsetBGM;
    public PetStatus petStatus;

    [Serializable]
    public struct formMusicData
    {
        public bool hasTwoTracks;
        public AudioClip normalMusic;
        public AudioClip upsetMusic;
    }
    [SerializeField]
    public List<formMusicData> petFormMusicData;

    private bool twoSongs;
    private bool onNormalSong;
    private float songFreeze;
    private bool fading;
    private int currentMusicDataIndex;

    [SerializeField]
    private float FADE_DURATION = 2;
    [SerializeField]
    private float NORMAL_FREEZE = 2;
    [SerializeField]
    private float UPSET_FREEZE = 10;

    // Start is called before the first frame update
    void Start()
    {
        twoSongs = false;
        onNormalSong = true;
        songFreeze = 0;
        fading = false;
        currentMusicDataIndex = 0;

        formMusicData startingData = petFormMusicData[0];
        switchMusicClips(startingData, true);
    }

    private void switchMusicClips(formMusicData newMusicData, bool init)
    {
        if (!init)
        {
            // fade out instead?
            normalBGM.Stop();
            upsetBGM.Stop();
            fading = false;
        }

        normalBGM.clip = newMusicData.normalMusic;
        upsetBGM.clip = newMusicData.upsetMusic;
        twoSongs = newMusicData.hasTwoTracks;
        
        if (!twoSongs || onNormalSong)
        {
            normalBGM.volume = 1;
            upsetBGM.volume = 0;
            normalBGM.PlayDelayed(1);
            songFreeze = NORMAL_FREEZE;
        } else
        {
            normalBGM.volume = 0;
            upsetBGM.volume = 1;
            upsetBGM.PlayDelayed(1);
            songFreeze = UPSET_FREEZE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // crossfading
        if (fading)
        {
            float fadeDelta = Time.deltaTime / FADE_DURATION;
            if (onNormalSong)
            {
                normalBGM.volume += fadeDelta;
                upsetBGM.volume -= fadeDelta;
                if (normalBGM.volume >= 1 && upsetBGM.volume <= 0)
                {
                    normalBGM.volume = 1;
                    upsetBGM.volume = 0;
                    upsetBGM.Stop();
                    fading = false;
                }
            } else
            {
                normalBGM.volume -= fadeDelta;
                upsetBGM.volume += fadeDelta;
                if (normalBGM.volume <= 0 && upsetBGM.volume >= 1)
                {
                    normalBGM.volume = 0;
                    upsetBGM.volume = 1;
                    normalBGM.Stop();
                    fading = false;
                }
            }
        }
        

        // Switching songs
        if (songFreeze > 0)
        {
            songFreeze -= Time.deltaTime;
        } else if (twoSongs)
        {
            bool notUpset = petStatus.isSatisfied() && !petStatus.isHungry();
            if (onNormalSong && !notUpset)
            {
                fadeToUpset();
            } else if (!onNormalSong && notUpset)
            {
                fadeToNormal();
            }
        }
    }

    public void updateFormMusicData(int petFormIndex)
    {
        if (petFormIndex != currentMusicDataIndex)
        {
            currentMusicDataIndex = petFormIndex;
            switchMusicClips(petFormMusicData[petFormIndex], false);
        }
    }
    
    private void fadeToUpset()
    {
        onNormalSong = false;
        songFreeze = UPSET_FREEZE;
        upsetBGM.Play();
        fading = true;
    }
    private void fadeToNormal()
    {
        onNormalSong = true;
        songFreeze = NORMAL_FREEZE;
        normalBGM.Play();
        fading = true;
    }
}

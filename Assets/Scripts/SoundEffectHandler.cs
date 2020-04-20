using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHandler : MonoBehaviour
{
    public static SoundEffectHandler player;

    public AudioSource soundEffectSource;
    public PetFormController petForm;

    [Serializable]
    public struct PetFormSfx
    {
        public List<AudioClip> neutralSoundEffects;
        public List<AudioClip> upsetSoundEffects;
    }
    [SerializeField]
    public List<PetFormSfx> petFormSoundEffects;


    // Start is called before the first frame update
    void Start()
    {
        SoundEffectHandler.player = this;
    }

    public void playRandomNeutralSound()
    {
        if (!soundEffectSource.isPlaying)
        {
            int petFormIndex = petForm.getPetFormIndex();
            List<AudioClip> soundEffectOptions =
                petFormSoundEffects[petFormIndex].neutralSoundEffects;
            int randomIndex = UnityEngine.Random.Range(0, soundEffectOptions.Count);
            AudioClip newEffect = soundEffectOptions[randomIndex];

            soundEffectSource.PlayOneShot(newEffect, 1);
        }
    }
}

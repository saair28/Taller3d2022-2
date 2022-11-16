using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip musicClip, shotSFX, endRoundSFX, satanVoice;

    [Header("Audio Mixer")]
    public AudioMixerGroup masterGroup;
    public Slider masterVolumeSlider;
    public float masterVolume;
    public AudioMixerGroup musicGroup;
    public Slider musicVolumeSlider;
    public float musicVolume;
    public AudioMixerGroup sfxGroup;
    public Slider sfxVolumeSlider;
    public float sfxVolume;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicClip = (AudioClip)Resources.Load("Audio/Music/testAudio");
        shotSFX = (AudioClip)Resources.Load("Audio/SFX/billSound");
        endRoundSFX = (AudioClip)Resources.Load("Audio/SFX/endRoundSFX");
        satanVoice = (AudioClip)Resources.Load("Audio/SFX/satanVoice");

        ChangeMusic(musicClip);
        UpdateMixerVolume();

        // PARA BAJAR EL VOLUMEN PORQUE ESTÁ MU ALTO.
        masterVolumeSlider.value = 0.05f;
    }

    public void PlaySFX(AudioSource source, AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    public void ChangeMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void UpdateMixerVolume()
    {
        masterVolume = masterVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;
        masterGroup.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        sfxGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    public void ChangeMasterVolume(float value)
    {
        masterVolume = value;
        masterGroup.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    public void ChangeMusicVolume(float value)
    {
        musicVolume = value;
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void ChangeSFXVolume(float value)
    {
        sfxVolume = value;
        sfxGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] musicClips;
    public List<AudioClip> musicClipsGame = new List<AudioClip>();
    public AudioClip bossMusic;
    //public AudioClip shotSFX;
    public AudioClip playerHitSFX;
    public AudioClip[] satanVoices;

    [Header("Audio Mixer")]
    public AudioMixerGroup masterGroup;
    public Slider masterVolumeSlider;
    public float masterVolume;
    public AudioMixerGroup musicGroup;
    public Slider musicVolumeSlider;
    public float musicVolume;
    public AudioMixerGroup voiceGroup;
    public Slider voiceVolumeSlider;
    public float voiceVolume;
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
        //musicClip = (AudioClip)Resources.Load("Audio/Music/testAudio");
        //shotSFX = (AudioClip)Resources.Load("Audio/SFX/billSound");
        //endRoundSFX = (AudioClip)Resources.Load("Audio/SFX/endRoundSFX");
        //satanVoice = (AudioClip)Resources.Load("Audio/SFX/satanVoice");

        AddArrayToList(musicClipsGame, musicClips);

        //ChangeMusic(musicClips);
        UpdateMixerVolume();

        // PARA BAJAR EL VOLUMEN PORQUE ESTÁ MU ALTO.
        masterVolumeSlider.value = 0.75f;
    }

    void AddArrayToList(List<AudioClip> clipsGame, AudioClip[] clips)
    {
        for (int i = 0; i< clips.Length; i++)
        {
            clipsGame.Add(clips[i]);
        }
    }

    private void Update()
    {
        if(!musicSource.isPlaying)
        {
            if(musicClipsGame.Count <= 0)
            {
                AddArrayToList(musicClipsGame, musicClips);
            }
            ChangeMusic(musicClipsGame);

        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioSource source, AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    public void PlaySFXWithDelay(AudioSource source, AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.PlayDelayed(0.5f);
    }

    public void PlaySFXOnce(AudioSource source, AudioClip clip, float volume)
    {
        if(source.isPlaying)
        {
            source.Stop();
        }
        source.clip = clip;
        source.volume = volume;
        source.PlayOneShot(clip);
    }

    public void ChangeMusic(List<AudioClip> clips)
    {
        int randomNumber = Random.Range(0, clips.Count);
        musicSource.clip = clips[randomNumber];
        musicSource.Play();
        clips.RemoveAt(randomNumber);
    }

    public void ChangeMusicBoss()
    {
        musicSource.clip = bossMusic;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void UpdateMixerVolume()
    {
        masterVolume = masterVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        voiceVolume = voiceVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;

        masterGroup.audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        voiceGroup.audioMixer.SetFloat("VoiceVolume", Mathf.Log10(voiceVolume) * 20);
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

    public void ChangeVoiceVolume(float value)
    {
        //voiceVolume = value;
        voiceVolume = voiceVolumeSlider.value;
        voiceGroup.audioMixer.SetFloat("VoiceVolume", Mathf.Log10(voiceVolume) * 20);
    }

    public void ChangeSFXVolume(float value)
    {
        sfxVolume = value;
        sfxGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }
}

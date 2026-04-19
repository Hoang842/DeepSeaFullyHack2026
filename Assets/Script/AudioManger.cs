// ------------------------------------------------------------
// Author: Hoang Nguyen
// Script: AudioManager.cs
// Purpose: Manages all game audio — background music, sound effects,
//          and volume settings that persist across scenes.
// Source: YouTube Video - "Unity Audio Tutorial"
// Channel: Làm Game Dạo
// URL: https://www.youtube.com/watch?v=8K_QWs1Hj5A&t=1994s
// Date Accessed: November 2, 2025
// Modifications by Hoang Nguyen:
// - PlayerPrefs saving for persistent volume settings
// - Slider self-registration via AudioSliderBinder (no tag lookups)
// - Persistent singleton with DontDestroyOnLoad
// ------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip losingClip;
    [SerializeField] private AudioClip winningClip;
    [SerializeField] private AudioClip enemyAttack;
    [SerializeField] private AudioClip CutSound;
    [SerializeField] private AudioClip BagUseSound;



    [Header("Volume Sliders (Registered at runtime)")]
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider effectSlider;

    public static AudioManager instance;

    private void Awake()
    {
        // Singleton pattern to keep AudioManager persistent
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Load saved volumes from PlayerPrefs
        float savedBgm = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float savedSfx = PlayerPrefs.GetFloat("SFXVolume", 1f);
        backgroundAudioSource.volume = savedBgm;
        effectAudioSource.volume = savedSfx;
    }


    private void Start()
    {
        PlayBackGroundMusic();
    }

    // ---------------- MUSIC & SOUND ----------------
    public void PlayBackGroundMusic()
    {
        if (backgroundAudioSource.clip != backGroundClip)
        {
            backgroundAudioSource.clip = backGroundClip;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
    }

    public void PlayJumpSound() => effectAudioSource.PlayOneShot(jumpClip);
    public void PlayLosingSound() => effectAudioSource.PlayOneShot(losingClip);
    public void PlayWinningSound() => effectAudioSource.PlayOneShot(winningClip);
    public void PlayEnemyAttackSound() => effectAudioSource.PlayOneShot(enemyAttack);
    public void PlayPortalSound() => effectAudioSource.PlayOneShot(CutSound);
    public void PlayMurhroomSound() => effectAudioSource.PlayOneShot(BagUseSound);

    // ---------------- VOLUME SETTINGS ----------------
    public void SetBackgroundVolume(float volume)
    {
        backgroundAudioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetEffectVolume(float volume)
    {
        effectAudioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    // ---------------- SLIDER REGISTRATION (Option A) ----------------
    public void RegisterBgmSlider(Slider slider)
    {
        // Detach our handler from any previous slider
        if (backgroundSlider != null)
            backgroundSlider.onValueChanged.RemoveListener(SetBackgroundVolume);

        backgroundSlider = slider;

        // Sync UI to current volume, then bind our handler
        backgroundSlider.value = backgroundAudioSource.volume;
        backgroundSlider.onValueChanged.RemoveListener(SetBackgroundVolume);
        backgroundSlider.onValueChanged.AddListener(SetBackgroundVolume);
    }

    public void RegisterSfxSlider(Slider slider)
    {
        if (effectSlider != null)
            effectSlider.onValueChanged.RemoveListener(SetEffectVolume);

        effectSlider = slider;

        effectSlider.value = effectAudioSource.volume;
        effectSlider.onValueChanged.RemoveListener(SetEffectVolume);
        effectSlider.onValueChanged.AddListener(SetEffectVolume);
    }

}

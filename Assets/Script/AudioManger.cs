using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip turtllesave;
    [SerializeField] private AudioClip Octosave;

    [SerializeField] private AudioClip losingClip;
    [SerializeField] private AudioClip winningClip;
    [SerializeField] private AudioClip Stun;
    [SerializeField] private AudioClip CutSound;
    [SerializeField] private AudioClip BagUseSound;

    [Header("Volume Sliders (Registered at runtime)")]
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider effectSlider;

    public static AudioManager instance;

    private void Awake()
    {
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

        float savedBgm = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float savedSfx = PlayerPrefs.GetFloat("SFXVolume", 1f);

        backgroundAudioSource.volume = savedBgm;
        effectAudioSource.volume = savedSfx;
    }

    private void Start()
    {
        PlayBackGroundMusic();
    }

    public void PlayBackGroundMusic()
    {
        if (backgroundAudioSource.clip != backGroundClip)
        {
            backgroundAudioSource.clip = backGroundClip;
            backgroundAudioSource.loop = true;
        }

        if (!backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Play();
        }
    }
    public void PlayTurtleSound() => effectAudioSource.PlayOneShot(turtllesave);

    public void PlayOctoSound() => effectAudioSource.PlayOneShot(Octosave);
    public void PlayLosingSound() => effectAudioSource.PlayOneShot(losingClip);
    public void PlayWinningSound() => effectAudioSource.PlayOneShot(winningClip);
    public void PlayStunSound() => effectAudioSource.PlayOneShot(Stun);
    public void PlayCutSound() => effectAudioSource.PlayOneShot(CutSound);
    public void PlayBagUseSound() => effectAudioSource.PlayOneShot(BagUseSound);

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

    public void RegisterBgmSlider(Slider slider)
    {
        if (backgroundSlider != null)
            backgroundSlider.onValueChanged.RemoveListener(SetBackgroundVolume);

        backgroundSlider = slider;

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
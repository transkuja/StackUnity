using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager s_instance = null;

    public AudioSource sourceFX;

    public AudioClip coinFX;
    public AudioClip failFX;


    [SerializeField]
    private float volumeMusic = 0.015f;
    [SerializeField]
    private float volumeFXs = 0.01f;


    private float currentVolume;

    public static AudioManager Instance
    {
        get
        {
            return s_instance;
        }
    }

    public float VolumeFXs
    {
        get
        {
            return volumeFXs;
        }

        set
        {
            volumeFXs = value;
            sourceFX.volume = volumeFXs;
        }
    }

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
    }

    void Start()
    {
        currentVolume = volumeMusic;
    }

    public void PlayOneShot(AudioClip clip)
    {
        sourceFX.PlayOneShot(clip, volumeFXs);
    }

    public void PlayOneShot(AudioClip clip, float volumeMultiplier)
    {
        sourceFX.PlayOneShot(clip, volumeFXs * volumeMultiplier);
    }

    public void Play(AudioClip clip, float volumeMultiplier = 1.0f)
    {
        if (sourceFX.clip != clip)
            sourceFX.clip = clip;
        sourceFX.volume = volumeFXs * volumeMultiplier;
        sourceFX.Play();
    }

}

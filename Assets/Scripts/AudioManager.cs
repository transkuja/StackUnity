using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager s_instance = null;

    public AudioSource sourceFX;

    public AudioClip coinFX;
    public AudioClip failFX;

    [SerializeField]
    private float volumeFXs = 0.01f;

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

    public void PlayOneShot(AudioClip clip, int currentCombo)
    {
        if (clip == failFX)
            sourceFX.pitch = 0.4f;
        else
            sourceFX.pitch = Mathf.Min(1 + 0.1f * currentCombo, 1.8f);
        sourceFX.PlayOneShot(clip, volumeFXs);
    }

}

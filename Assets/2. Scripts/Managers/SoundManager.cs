using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    BGM,
    BGM2,
    SFX,
    Max
}

[System.Serializable]
public struct AudioClips
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource[] audioSources = new AudioSource[(int)Sound.Max];
    [SerializeField] private List<AudioClips> bgmClips;
    [SerializeField] private List<AudioClips> sfxClips;
    public float BGMVolume = 0.5f;
    public float SFXVolume = 0.5f;

    public AudioSource BGM => audioSources[(int)Sound.BGM];
    public AudioSource BGM2 => audioSources[(int)Sound.BGM2];
    public AudioSource SFX => audioSources[(int)Sound.SFX];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        BGM.loop = true;
        BGM2.loop = true;
    }

    private void Start()
    {
        Play("MainBGM", Sound.BGM);
        Play("Rain", Sound.BGM2);
    }

    public void ClickBtnSFX()
    {
        Play("Click");
    }

    private void Update()
    {
        BGM.volume = BGMVolume;
        BGM2.volume = BGMVolume * 0.3f;
        SFX.volume = SFXVolume;
    }

    public void Play(string name, Sound type = Sound.SFX)
    {
        Debug.Log("Play");
        AudioClip audioClip = GetAudioClip(name, type);

        if (type == Sound.SFX)
        {
            AudioSource audioSource = SFX;

            audioSource.volume = SFXVolume;
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            AudioSource audioSource;
            if (type == Sound.BGM)
            {
                audioSource = BGM;
                audioSource.volume = BGMVolume;
            }
            else
            {
                audioSource = BGM2;
                audioSource.volume = BGMVolume * 0.3f;
            }
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    private AudioClip GetAudioClip(string name, Sound type = Sound.SFX)
    {
        List<AudioClips> getList = null;

        if (type == Sound.SFX)
            getList = sfxClips;
        else
            getList = bgmClips;

        AudioClip getClip = getList.Find(x => x.name.Equals(name)).clip;
        return getClip;
    }
}

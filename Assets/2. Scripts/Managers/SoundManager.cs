using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    BGM,
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
    public AudioSource SFX => audioSources[(int)Sound.SFX];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        BGM.loop = true;
    }

    public void ClickBtnSFX()
    {
        SoundManager.instance.Play("Click");
    }

    private void Update()
    {
        BGM.volume = BGMVolume;
        SFX.volume = SFXVolume;
    }

    public void Play(string name, Sound type = Sound.SFX)
    {
        Debug.Log("Play");
        AudioClip audioClip = GetAudioClip(name, type);

        if (type == Sound.BGM)
        {
            AudioSource audioSource = BGM;
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.volume = BGMVolume;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // SFX
        {
            AudioSource audioSource = SFX;

            audioSource.volume = SFXVolume;
            audioSource.PlayOneShot(audioClip);
        }
    }

    private AudioClip GetAudioClip(string name, Sound type = Sound.SFX)
    {
        List<AudioClips> getList = null;

        if (type == Sound.BGM)
            getList = bgmClips;
        else
            getList = sfxClips;

        AudioClip getClip = getList.Find(x => x.name.Equals(name)).clip;
        return getClip;
    }
}

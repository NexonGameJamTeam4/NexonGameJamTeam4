using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        bgmSlider.onValueChanged.AddListener(BGMValueChanged);
        sfxSlider.onValueChanged.AddListener(SFXValueChanged);
    }

    public void BGMValueChanged(float value)
    {
        SoundManager.instance.BGMVolume = value;
    }

    public void SFXValueChanged(float value)
    {
        SoundManager.instance.SFXVolume = value;
    }
}

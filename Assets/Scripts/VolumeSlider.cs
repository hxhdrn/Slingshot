using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    AudioManager audioScript;

    private void Start()
    {
        audioScript = FindObjectOfType<AudioManager>();

        // set slider to default volume
        slider.value = audioScript.sliderVal;
        SetVolume(audioScript.sliderVal);
    }

    // called by slider UI element
    public void SetVolume(float sliderVal)
    {
        if (audioScript != null)
        {
            // uses log to make slider feel more balanced
            mixer.SetFloat("Volume", Mathf.Log10(sliderVal) * 20);
            audioScript.sliderVal = sliderVal;
        }
    }
}

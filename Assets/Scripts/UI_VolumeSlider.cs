using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string auidoParametr;
    [SerializeField] private float multiplier = 25;

    public void SetupSlider()
    {
        slider.onValueChanged.AddListener(SliderValue); // uı daki slider ile sfx ve backgroundu ayarlayamaya yarayacak.
        slider.minValue = .001f; // en düşük seviyesi
        slider.value = PlayerPrefs.GetFloat(auidoParametr, slider.value); // default value ve upload ettik.
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(auidoParametr, slider.value); // kaydediyoruz settingsten çıkınca

    }
    private void SliderValue(float value)
    {
        audioMixer.SetFloat(auidoParametr, Mathf.Log10(value) * multiplier);

    }


}

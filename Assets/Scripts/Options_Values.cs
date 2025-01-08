using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Values : MonoBehaviour
{
    public Slider volume_slider;
    public float sliderValue;
    
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Volume_Music"))
        {
            sliderValue = 1.0f;
            PlayerPrefs.SetFloat("Volume_Music", sliderValue);
            PlayerPrefs.Save();
        }
        else
        {
            sliderValue = PlayerPrefs.GetFloat("Volume_Music");
        }

        AudioListener.volume = sliderValue;

        if (volume_slider != null)
        {
            volume_slider.value = sliderValue;
        }
    }

    // Update is called once per frame
    public void ChangeSlider(float val)
    {
        sliderValue = val;
        PlayerPrefs.SetFloat("Volume_Music", sliderValue);
        AudioListener.volume = sliderValue;
    }
}

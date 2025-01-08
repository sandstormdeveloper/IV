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
        if(!PlayerPrefs.HasKey("Volume_music"))
        {
            sliderValue = 0.5f;
            PlayerPrefs.SetFloat("Volume_Music", sliderValue);
            PlayerPrefs.Save();
        }
        else
        {
            sliderValue = PlayerPrefs.GetFloat("Volume_Music", 0.5f);

        }

        AudioListener.volume = sliderValue;
    }

    // Update is called once per frame
    public void ChangeSlider(float val)
    {
        sliderValue = val;
        PlayerPrefs.SetFloat("Volume_Music", sliderValue);
        AudioListener.volume = sliderValue;
    }
}

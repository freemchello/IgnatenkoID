using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    AudioSource audioSource;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (slider.value != audioSource.volume)
        {
            audioSource.volume = slider.value;
        }
    }
}

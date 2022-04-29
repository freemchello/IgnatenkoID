using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepSoundControl : MonoBehaviour
{
    private AudioSource _source;


    [SerializeField] private AudioClip[] _footSteps;
    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Step()
    {
        var stepSound = _footSteps[Random.Range(0, _footSteps.Length)];

        //_source.clip = stepSound;
        //_source.Play();

        _source.PlayOneShot(stepSound);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SmashSound : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] private AudioClip[] _grenade;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.PlayOneShot(_grenade[Random.Range(0, _grenade.Length)]);
    }
    
    //public void Boom()
    //{
    //    var mineSound = _grenade[Random.Range(0, _grenade.Length)];
    //    _source.PlayOneShot(mineSound);
    //}
}

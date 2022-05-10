using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager audioManager;

    [SerializeField] private AudioSource _source;

    private AudioManager() {

    }

    public static AudioManager GetInstance() {
        return audioManager;
    }

    private void Start() {
        audioManager = this;
    }
    
    public void Play(AudioClip clip) {
        _source.clip = clip;
        _source.Play();
    }
}

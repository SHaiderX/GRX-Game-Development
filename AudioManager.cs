using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Dictionary<Sound, float> volumes = new Dictionary<Sound, float>();

    public static AudioManager instance;

    public bool Mute = false;

    void Awake() {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            volumes.Add(s, s.volume);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        Play("Theme");
    }

    public void Volume(Sound s, float vol) {

        if(s.source.volume != vol)
            s.source.volume = vol;
    }

    public void Play(string name) {


        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            UnityEngine.Debug.Log("Sound:" + name + " not found");
        else
            s.source.Play();

    }

    public void Stop(string name)
    {


        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            UnityEngine.Debug.Log("Sound:" + name + " not found");
        else
            s.source.Stop();

    }

    //Buttons

    public void MuteSong()
    {
        Mute = true;
        Stop("Theme");
    }

    public void UnmuteSong()
    {
        Mute = false;
        Play("Theme");
    }

    public void MuteAll()
    {
        Mute = true;
        UnityEngine.Debug.Log("Muted");
        foreach (Sound s in sounds)
        {
            Volume(s, 0f);
            if (s.name == "Theme")
                Volume(s, 0.3f);
        }
    }

    public void UnmuteAll()
    {
        Mute = false;
        UnityEngine.Debug.Log("Unmuted");
        float vol;
        foreach (Sound s in sounds)
        {
            vol = volumes[s];
            Volume(s, vol);
        }
    }

}

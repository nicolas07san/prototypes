using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
            
        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

    }

    public void Play(string name)
    {
        if(!this.IsPlaying(name)){
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found.");
                return;
            }
            
            s.source.Play();
        }
    }

    public void Play(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.pitch = pitch;
        s.source.Play();
    }

    public void Stop(string name)
    {
        if(this.IsPlaying(name)){
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if(s == null)
            {
                Debug.LogWarning("Sound: " + name + "not found.");
                return;
            }

            s.source.Stop();
        }
        
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found.");
        }

        return s.source.isPlaying;
    }
}

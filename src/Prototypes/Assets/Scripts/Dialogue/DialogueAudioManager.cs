using System;
using UnityEngine;
using UnityEngine.Audio;

public class DialogueAudioManager : MonoBehaviour
{
    public DialogueAudio[] dialogues;

    public static DialogueAudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(DialogueAudio dialogue in dialogues)
        {
            foreach(Sound sound in dialogue.lines)
            {
                sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }
            
        }
    }

    public void Play(string name, int lineNumber)
    {
        DialogueAudio dialogue = Array.Find(dialogues, dialogue => dialogue.name == name); 
        Sound s = dialogue.lines[lineNumber];

        if (s == null)
        {
            Debug.LogWarning("Dialogue " + name + " line " + lineNumber + " not found");
            return;
        }
            
        s.source.Play();
    }

    public void Stop(string name, int lineNumber)
    {
        DialogueAudio dialogue = Array.Find(dialogues, dialogue => dialogue.name == name); 
        Sound s = dialogue.lines[lineNumber];

        if (s == null)
        {
            Debug.LogWarning("Dialogue " + name + " line " + lineNumber + " not found");
            return;
        }

        s.source.Stop();

    }

    public bool IsPlaying(string name, int lineNumber)
    {
        DialogueAudio dialogue = Array.Find(dialogues, dialogue => dialogue.name == name); 
        Sound s = dialogue.lines[lineNumber];

        if (s == null)
        {
            Debug.LogWarning("Dialogue " + name + " line " + lineNumber + " not found");
        }

        return s.source.isPlaying;
    }
}

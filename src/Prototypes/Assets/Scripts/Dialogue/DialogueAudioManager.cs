using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    public DialogueAudio[] dialogues;

    public static DialogueAudioManager instance;

    private string currentDialogueName;

    private DialogueAudio currentDialogue;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        currentDialogueName = LevelManager.instance.level.inkJsonFile.name;

        foreach(DialogueAudio dialogue in dialogues)
        {
            if(dialogue.name == currentDialogueName)
            {
                currentDialogue = dialogue;

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
    }

    public void Play(int lineNumber)
    {
        Sound s = currentDialogue.lines[lineNumber];

        if (s == null)
        {
            Debug.LogWarning("Dialogue " + currentDialogueName + " line " + lineNumber + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(int lineNumber)
    { 
        Sound s = currentDialogue.lines[lineNumber];

        if (s == null)
        {
            Debug.LogWarning("Dialogue " + currentDialogueName + " line " + lineNumber + " not found");
            return;
        }

        s.source.Stop();

    }

    public bool IsPlaying(int lineNumber)
    {
        Sound s = currentDialogue.lines[lineNumber];

        if (s == null)
        {
            Debug.LogWarning("Dialogue " + currentDialogueName + " line " + lineNumber + " not found");
        }

        return s.source.isPlaying;
    }
}

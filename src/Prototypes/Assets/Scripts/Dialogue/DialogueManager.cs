using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private TextAsset inkJsonFile;

    private Story currentStory;

    private bool dialogueIsPlaying;
    
    private void Start()
    {
        LoadStory(inkJsonFile);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ContinueStory();
        }
    }

    public void LoadStory(TextAsset inkJSON)
    {
        currentStory =  new Story(inkJSON.text);
        ContinueStory();
    }

    public void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            dialogueText.text = "Fim da história";
        }
    }

}

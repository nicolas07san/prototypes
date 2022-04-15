using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private TextAsset inkJsonFile;

    [SerializeField] private Animator portraitAnimator;

    [SerializeField] private Animator layoutAnimator;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "position";

    private Story currentStory;
    
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

    private void LoadStory(TextAsset inkJSON)
    {
        currentStory =  new Story(inkJSON.text);
        ContinueStory();
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            dialogueText.text = "Fim da história";
        }
    }

    private void HandleTags(List<string> currentTags)
    {

        foreach(string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            switch(tagKey)
            {
                case SPEAKER_TAG:
                    nameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
                
        }
    }

}

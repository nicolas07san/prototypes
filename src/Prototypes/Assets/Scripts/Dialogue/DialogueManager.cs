using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private Animator layoutAnimator;

    [Header("Story")]
    [SerializeField] private TextAsset inkJsonFile;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "position";

    private Story currentStory;

    private Coroutine displayLineCoroutine;

    private bool canContinueToNextLine = false;
    private bool skipLine;

    private string dialogueName = "TestScene";
    private int lineNumber;
    
    private void Start()
    {
        LoadStory(inkJsonFile);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canContinueToNextLine)
        {
            if(DialogueAudioManager.instance.IsPlaying(dialogueName, lineNumber))
            {
                DialogueAudioManager.instance.Stop(dialogueName, lineNumber);
            }
            ContinueStory();
        }
        else if(Input.GetMouseButtonDown(0))
        {
            skipLine = true;
        }
    }

    private void LoadStory(TextAsset inkJSON)
    {
        currentStory =  new Story(inkJSON.text);
        lineNumber = -1;
        ContinueStory();
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            lineNumber += 1;

            if(displayLineCoroutine != null)
                StopCoroutine(displayLineCoroutine);

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
            
            DialogueAudioManager.instance.Play(dialogueName, lineNumber);
            
        }
        else
        {
            dialogueText.text = "...";
            LevelManager.instance.LoadScene("Combat");
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

    private IEnumerator DisplayLine(string line)
    {
        // empty the dialogue text
        dialogueText.text = "";

        //hide items while text is typing
        continueIcon.SetActive(false);

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach(char letter in line.ToCharArray())
        {
            if(skipLine)
            {
                dialogueText.text = line;
                break;
            }

            if(letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;

                if(letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }

        continueIcon.SetActive(true);

        skipLine = false;
        canContinueToNextLine = true;

    }

}

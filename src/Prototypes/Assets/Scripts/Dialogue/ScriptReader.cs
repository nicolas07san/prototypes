using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class ScriptReader : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJsonFile;
    private Story storyScript;

    public TMP_Text dialogueText;
    public TMP_Text nameTag;

    void Start()
    {
        LoadStory();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0))
        {
            DisplayNextLine();
        }
    }

    void LoadStory()
    {
        storyScript = new Story(inkJsonFile.text);
        storyScript.BindExternalFunction("Name", (string charName) => ChangeName(charName));
    }

    public void DisplayNextLine()
    {
        if(storyScript.canContinue) // Checa se tem mais texto
        {
            string text = storyScript.Continue(); // Pega a próxima linha
            text = text?.Trim(); // Remove espaço em branco do texto
            dialogueText.text = text;
        }
        else
        {
            dialogueText.text = "Fim da história";
        }
    }

    public void ChangeName(string name)
    {
        nameTag.text = name;

    }
}

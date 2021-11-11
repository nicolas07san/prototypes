using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    [SerializeField]private Button previousButton;
    [SerializeField]private Button nextButton;

    private int currentCard = 0;

    private void Awake()
    {
        SelectCard(0);
    }

    public void SelectCard(int index)
    {
        previousButton.interactable = (index != 0);
        nextButton.interactable = (index != transform.childCount - 1);

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);

        }
    }

    public void ChangeCard(int change)
    {
        currentCard += change;
        SelectCard(currentCard);

    } 
}

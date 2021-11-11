using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button selectButton;

    [SerializeField] private GameObject playerHand;

    private int currentCard = 0;

    private void Awake()
    {
        currentCard = 0;
        SelectCard(currentCard);
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

    public void SaveCard()
    {
        transform.GetChild(currentCard).gameObject.transform.SetParent(playerHand.transform);
    }

    public void ChangeCard(int change)
    {
        currentCard += change;
        SelectCard(currentCard);

    } 
}

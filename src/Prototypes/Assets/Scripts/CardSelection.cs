using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button undoButton;

    [SerializeField] private GameObject playerHand;

    private int currentCard = 0;

    private void Awake()
    {
        currentCard = 0;
        SelectCard(currentCard);
    }

    public void SelectCard(int index)
    {
        undoButton.interactable = (playerHand.transform.childCount > 0);
        selectButton.interactable = (playerHand.transform.childCount < 3);

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void SaveChosenCard()
    {

        GameObject card = transform.GetChild(currentCard).gameObject;
        Vector3 distance = new Vector3(-25 * playerHand.transform.childCount, -25 * playerHand.transform.childCount);
        card.transform.position = Vector3.Lerp(card.transform.position, playerHand.transform.position + distance, 1f);

        card.transform.SetParent(playerHand.transform);
        ChangeCard(0);
        undoButton.interactable = (playerHand.transform.childCount > 0);

    }

    public void UndoChosenCard()
    {
        GameObject card = playerHand.transform.GetChild(playerHand.transform.childCount-1).gameObject;
        card.transform.position = Vector3.Lerp(card.transform.position, transform.position, 1f);

        card.SetActive(false);
        card.transform.SetParent(transform);

        card.transform.SetSiblingIndex(0);
        ChangeCard(-currentCard);
        undoButton.interactable = (playerHand.transform.childCount > 0);
    }

    public void ChangeCard(int change)
    {
        currentCard += change;
        if (currentCard > transform.childCount - 1)
            currentCard = 0;
        else if (currentCard < 0)
            currentCard = transform.childCount - 1;

        Debug.Log("currentCard = " + currentCard);
            
        SelectCard(currentCard);

    }
    
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

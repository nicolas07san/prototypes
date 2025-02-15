using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button selectButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private Button confirmButton;

    [Header("Game Objects")]
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject enemyHand;

    private int currentCard = 0;

    private void Awake()
    {
        currentCard = 0;
        SelectCard(currentCard);
    }

    private void Start()
    {
        AudioManager.instance.Stop("VictorySound");
        AudioManager.instance.Stop("DefeatSound");
        AudioManager.instance.Play("MenuTheme");
    }

    public void SelectCard(int index)
    {
        undoButton.interactable = (playerHand.transform.childCount > 0);
        selectButton.interactable = (playerHand.transform.childCount < 3);
        confirmButton.interactable = (playerHand.transform.childCount == 3);

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void SaveChosenCard()
    {

        GameObject card = transform.GetChild(currentCard).gameObject;
        Vector3 distance = new Vector3(-0.8f * playerHand.transform.childCount, -0.8f * playerHand.transform.childCount);
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

        AudioManager.instance.Play("CardFlip");
            
        SelectCard(currentCard);

    }
    
    public void BackButton()
    {
        LevelManager.instance.LoadScene("MainMenu");
        // AudioManager.instance.Stop("MenuTheme");
    }

    public void ConfirmButton()
    {
        for(int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, transform.childCount - 1);
            GameObject card = transform.GetChild(index).gameObject;
            card.SetActive(false);
            card.transform.SetParent(enemyHand.transform);

            playerHand.transform.GetChild(i).gameObject.SetActive(false);
        }

        Object.Instantiate(playerHand, LevelManager.instance.transform);
        Object.Instantiate(enemyHand, LevelManager.instance.transform);
        
        LevelManager.instance.LoadScene("Combat");

        AudioManager.instance.Stop("MenuTheme");
    }
}

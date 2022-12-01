using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private GameObject pageHolder;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    private int currentPage;

    void Start()
    {
        currentPage = 0;
        SelectPage(currentPage);
    }

    public void SelectPage(int index)
    {
        for(int i = 0; i < pageHolder.transform.childCount; i++)
        {
            pageHolder.transform.GetChild(i).gameObject.SetActive(i == index);
        }

        nextButton.interactable = (currentPage < transform.childCount);
        previousButton.interactable = (currentPage > 0);
    }

    public void ChangePage(int change)
    {
        currentPage += change;
        
        SelectPage(currentPage);

    }

}

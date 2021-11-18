using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject enemyHand;

    [SerializeField] private TMPro.TMP_Text diceText;
    [SerializeField] private TMPro.TMP_Text roundText;

    private Vector3 playerHandPosition = new Vector3(-600, 0);
    private Vector3 enemyHandPosition = new Vector3(600, 0);

    private int randomNumber;

    private int round = 1;

    void Start()
    {
        playerHand.transform.localPosition = playerHandPosition;
        enemyHand.transform.localPosition = enemyHandPosition;

        InitialCardPlacement(playerHand);
        InitialCardPlacement(enemyHand);

        StartCoroutine(StartRoundAnimation());
        
    }
    
    private void InitialCardPlacement(GameObject cardHand)
    {

        for(int i = 0; i < cardHand.transform.childCount; i++)
        {

            GameObject card = cardHand.transform.GetChild(i).gameObject;

            if(i == 0)
            {
                card.transform.localPosition = Vector3.zero;
            }
            else if(i == 1)
            {
                card.transform.localScale = new Vector3(0.5f, 0.5f);
                card.transform.localPosition = new Vector3(100, -500);
            }
            else if (i == 2)
            {
                card.transform.localScale = new Vector3(0.5f, 0.5f);
                card.transform.localPosition = new Vector3(-100, -500);
            }


            card.SetActive(true);

        }
    }

    public IEnumerator StartRoundAnimation() {
        Debug.Log("Coroutine Iniciada");
        Vector3 startPosition = new Vector3(0, -200);
        Vector3 finalPosition = new Vector3(0, 200);

        roundText.gameObject.SetActive(true);
        roundText.transform.localPosition = startPosition;
        roundText.text = "Round " + round;
        

        while(Vector3.Distance(roundText.transform.localPosition, finalPosition) > 0.05f)
        {
            roundText.transform.localPosition = Vector3.Lerp(roundText.transform.localPosition, finalPosition, 0.0125f);
            Debug.Log("Dentro do While");
            yield return null;
        }

        roundText.transform.localPosition = finalPosition;
        roundText.gameObject.SetActive(false);

        StartCoroutine(RollDice());

        yield return null;

    }

    public IEnumerator RollDice()
    {

        for (int i = 0; i < 100; i++)
        {
            randomNumber = Random.Range(1, 6);
            diceText.text = randomNumber.ToString();
            yield return new WaitForSecondsRealtime(3f / 100);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject enemyHand;
    [SerializeField] private GameObject dice;

    private Vector3 playerHandPosition = new Vector3(-600, 0);
    private Vector3 enemyHandPosition = new Vector3(600, 0);

    void Start()
    {
        playerHand.transform.localPosition = playerHandPosition;
        enemyHand.transform.localPosition = enemyHandPosition;

        InitialCardPlacement(playerHand);
        InitialCardPlacement(enemyHand);
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
}

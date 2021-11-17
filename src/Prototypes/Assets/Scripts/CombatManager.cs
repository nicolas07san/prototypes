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
        playerHand.transform.position = playerHandPosition;
        enemyHand.transform.position = enemyHandPosition;

        PlaceCards(playerHand);
        PlaceCards(enemyHand);
    }
    
    private void PlaceCards(GameObject cardHand)
    {

        for(int i = 0; i < cardHand.transform.childCount; i++)
        {

            GameObject card = cardHand.transform.GetChild(i).gameObject;

            if(i == 0)
            {
                card.transform.position = cardHand.transform.position;
            }
            else
            {
                card.transform.localScale = new Vector3(0.5f, 0.5f);
                card.transform.position = cardHand.transform.position - new Vector3(50*i, 50);
            }

            card.SetActive(true);

        }
    }
}

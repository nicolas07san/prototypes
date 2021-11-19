using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject enemyHand;

    [SerializeField] private TMP_Text diceText;
    [SerializeField] private TMP_Text roundText;

    [Header("Player Stats")]
    [SerializeField] private TMP_Text playerManaText;
    [SerializeField] private TMP_Text playerShieldText;
    [SerializeField] private TMP_Text playerHealthText;

    [Header("Enemy")]
    [SerializeField] private TMP_Text enemyManaText;
    [SerializeField] private TMP_Text enemyShieldText;
    [SerializeField] private TMP_Text enemyHealthText;

    private Vector3 playerHandPosition = new Vector3(-700, 0);
    private Vector3 enemyHandPosition = new Vector3(700, 0);

    private int diceNumber;
    private int round = 1;

    // Player stats
    private int playerMana = 0;
    private int playerShield;
    private int playerHealth;
    private GameObject playerAtk1;
    private GameObject playerAtk2;
    private GameObject playerAtk3;

    // Enemy stats
    private int enemyMana = 0;
    private int enemyShield;
    private int enemyHealth;
    private GameObject enemyAtk1;
    private GameObject enemyAtk2;
    private GameObject enemyAtk3;

    void Start()
    {
        playerHand.transform.localPosition = playerHandPosition;
        enemyHand.transform.localPosition = enemyHandPosition;

        InitialCardPlacement(playerHand);
        InitialCardPlacement(enemyHand);
        GetInitialBasicStats();

        StartCoroutine(StartRoundAnimation());
        
    }
    
    private void InitialCardPlacement(GameObject cardHand)
    {

        for(int i = 0; i < cardHand.transform.childCount; i++)
        {

            GameObject card = cardHand.transform.GetChild(i).gameObject;

            if(i == 0)
            {
                card.transform.localPosition = new Vector3(0, 200);
            }
            else if(i == 1)
            {
                card.transform.localScale = new Vector3(0.5f, 0.5f);
                card.transform.localPosition = new Vector3(100, -300);
            }
            else if (i == 2)
            {
                card.transform.localScale = new Vector3(0.5f, 0.5f);
                card.transform.localPosition = new Vector3(-100, -300);
            }


            card.SetActive(true);

        }
    }

    public IEnumerator StartRoundAnimation() {

        Vector3 startPosition = new Vector3(0, -200);
        Vector3 finalPosition = new Vector3(0, 200);

        roundText.gameObject.SetActive(true);
        roundText.transform.localPosition = startPosition;
        roundText.text = "Round " + round;
        

        while(Vector3.Distance(roundText.transform.localPosition, finalPosition) > 0.05f)
        {
            roundText.transform.localPosition = Vector3.Lerp(roundText.transform.localPosition, finalPosition, 0.0125f);
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
            diceNumber = Random.Range(1, 6);
            diceText.text = diceNumber.ToString();
            yield return new WaitForSecondsRealtime(3f / 100);
        }

        playerMana += diceNumber;
        StartCoroutine(UpdateStatText(playerManaText, playerMana));

        enemyMana += diceNumber;
        StartCoroutine(UpdateStatText(enemyManaText, enemyMana));

        yield return null;
    }

    public void AttackController()
    {
        
        
    }

    public void GetInitialBasicStats()
    {
        GameObject playerCard = playerHand.transform.GetChild(0).gameObject;
        GameObject enemyCard = enemyHand.transform.GetChild(0).gameObject;

        playerShield = int.Parse(playerCard.transform.Find("Shield").GetComponent<TMP_Text>().text);
        playerShieldText.text = playerShield.ToString();

        playerHealth = int.Parse(playerCard.transform.Find("Health").GetComponent<TMP_Text>().text);
        playerHealthText.text = playerHealth.ToString();

        playerAtk1 = playerCard.transform.Find("Attack1").gameObject;
        playerAtk2 = playerCard.transform.Find("Attack2").gameObject;
        playerAtk3 = playerCard.transform.Find("Attack3").gameObject;

        enemyShield = int.Parse(enemyCard.transform.Find("Shield").GetComponent<TMP_Text>().text);
        enemyShieldText.text = enemyShield.ToString();

        enemyHealth = int.Parse(enemyCard.transform.Find("Health").GetComponent<TMP_Text>().text);
        enemyHealthText.text = enemyHealth.ToString();

        enemyAtk1 = enemyCard.transform.Find("Attack1").gameObject;
        enemyAtk2 = enemyCard.transform.Find("Attack2").gameObject;
        enemyAtk3 = enemyCard.transform.Find("Attack3").gameObject;

    }

    public IEnumerator UpdateStatText(TMP_Text statText,int statVariable)
    {
        int valueDifference = 0;

        if(int.Parse(statText.text) < statVariable)
        {
            valueDifference = int.Parse(statText.text) - statVariable;
            valueDifference *= -1;
        }
        else
        {
            valueDifference = int.Parse(statText.text) - statVariable;
        }
        

        if(valueDifference > 0)
        {
            for(int i = valueDifference; i > 0; i--)
            {
                statText.text = (int.Parse(statText.text) + 1).ToString();
                yield return new WaitForSeconds(3f/valueDifference);
            }
        }

        else if(valueDifference < 0)
        {
            for(int i = valueDifference; i < 0; i++)
            {
                statText.text = (int.Parse(statText.text) - 1).ToString();
                yield return new WaitForSeconds(3f/(valueDifference *-1));
            }
        }

        statText.text = statVariable.ToString();

        yield return null;
    }
}

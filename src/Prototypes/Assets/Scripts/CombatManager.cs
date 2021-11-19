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

    // Attack 1
    private Button playerAtk1Button;
    private int playerAtk1Cost;
    private int playerAtk1Damage;
    
    // Attack 2
    private Button playerAtk2Button;
    private int playerAtk2Cost;
    private int playerAtk2Damage;

    // Attack 3
    private Button playerAtk3Button;
    private int playerAtk3Cost;
    private int playerAtk3Damage;

    // Enemy stats
    private int enemyMana = 0;
    private int enemyShield;
    private int enemyHealth;

    // Attack 1
    private int enemyAtk1Cost;
    private int enemyAtk1Damage;

    // Attack 2
    private int enemyAtk2Cost;
    private int enemyAtk2Damage;

    // Attack 3
    private int enemyAtk3Cost;
    private int enemyAtk3Damage;

    void Start()
    {
        playerHand.transform.localPosition = playerHandPosition;
        enemyHand.transform.localPosition = enemyHandPosition;

        InitialCardPlacement(playerHand);
        InitialCardPlacement(enemyHand);

        StartCoroutine(GetInitialBasicStats());

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

        AttackController();
    }

    public void AttackController()
    {   
        if(playerMana >= playerAtk1Cost || playerMana >= playerAtk2Cost || playerMana >= playerAtk3Cost)
        {
            if (playerMana >= playerAtk1Cost)
                playerAtk1Button.interactable = true;

            if (playerMana >= playerAtk2Cost)
                playerAtk2Button.interactable = true;

            if (playerMana >= playerAtk3Cost)
                playerAtk3Button.interactable = true;
        }
        else
        {
            enemyAttackController();
        }

    }

    public IEnumerator GetInitialBasicStats()
    {
        GameObject playerCard = playerHand.transform.GetChild(0).gameObject;

        GameObject playerAtk1 = playerCard.transform.Find("Attack1").gameObject;
        GameObject playerAtk2 = playerCard.transform.Find("Attack2").gameObject;
        GameObject playerAtk3 = playerCard.transform.Find("Attack3").gameObject;

        yield return null;

        playerShield = int.Parse(playerCard.transform.Find("Shield").GetComponent<TMP_Text>().text);
        playerShieldText.text = playerShield.ToString();

        playerHealth = int.Parse(playerCard.transform.Find("Health").GetComponent<TMP_Text>().text);
        playerHealthText.text = playerHealth.ToString();

        yield return null;

        // Attack 1
        playerAtk1Button = playerAtk1.transform.Find("Button").GetComponent<Button>();
        playerAtk1Cost = int.Parse(playerAtk1.transform.Find("Cost").GetComponent<TMP_Text>().text);
        playerAtk1Damage = int.Parse(playerAtk1.transform.Find("Damage").GetComponent<TMP_Text>().text);

        playerAtk1Button.onClick.AddListener(delegate { playerAttack(playerAtk1Cost, playerAtk1Damage); });

        // Attack 2
        playerAtk2Button = playerAtk2.transform.Find("Button").GetComponent<Button>();
        playerAtk2Cost = int.Parse(playerAtk2.transform.Find("Cost").GetComponent<TMP_Text>().text);
        playerAtk2Damage = int.Parse(playerAtk2.transform.Find("Damage").GetComponent<TMP_Text>().text);

        playerAtk2Button.onClick.AddListener(delegate { playerAttack(playerAtk2Cost, playerAtk2Damage); });

        // Attack 3
        playerAtk3Button = playerAtk3.transform.Find("Button").GetComponent<Button>();
        playerAtk3Cost = int.Parse(playerAtk3.transform.Find("Cost").GetComponent<TMP_Text>().text);
        playerAtk3Damage = int.Parse(playerAtk3.transform.Find("Damage").GetComponent<TMP_Text>().text);

        playerAtk3Button.onClick.AddListener(delegate { playerAttack(playerAtk3Cost, playerAtk3Damage); });

        yield return null;

        GameObject enemyCard = enemyHand.transform.GetChild(0).gameObject;

        GameObject enemyAtk1 = enemyCard.transform.Find("Attack1").gameObject;
        GameObject enemyAtk2 = enemyCard.transform.Find("Attack2").gameObject;
        GameObject enemyAtk3 = enemyCard.transform.Find("Attack3").gameObject;

        yield return null;

        enemyShield = int.Parse(enemyCard.transform.Find("Shield").GetComponent<TMP_Text>().text);
        enemyShieldText.text = enemyShield.ToString();

        enemyHealth = int.Parse(enemyCard.transform.Find("Health").GetComponent<TMP_Text>().text);
        enemyHealthText.text = enemyHealth.ToString();

        yield return null;

        // Attack 1
        enemyAtk1Cost = int.Parse(enemyAtk1.transform.Find("Cost").GetComponent<TMP_Text>().text);
        enemyAtk1Damage = int.Parse(enemyAtk1.transform.Find("Damage").GetComponent<TMP_Text>().text);

        // Attack 2
        enemyAtk2Cost = int.Parse(enemyAtk2.transform.Find("Cost").GetComponent<TMP_Text>().text);
        enemyAtk2Damage = int.Parse(enemyAtk2.transform.Find("Damage").GetComponent<TMP_Text>().text);

        // Attack 3
        enemyAtk3Cost = int.Parse(enemyAtk3.transform.Find("Cost").GetComponent<TMP_Text>().text);
        enemyAtk3Damage = int.Parse(enemyAtk3.transform.Find("Damage").GetComponent<TMP_Text>().text);

        yield return null;

        StartCoroutine(StartRoundAnimation());

    }

    public IEnumerator UpdateStatText(TMP_Text statText,int statVariable)
    {
        int valueDifference = 0;
        float animationTime = 1f;

        if(statVariable > int.Parse(statText.text))
        {
            valueDifference = int.Parse(statText.text) - statVariable;
            valueDifference *= -1;
        }
        else
        {
            valueDifference = int.Parse(statText.text) - statVariable;
            valueDifference *= -1;
        }
        

        if(valueDifference > 0)
        {
            for(int i = valueDifference; i > 0; i--)
            {
                statText.text = (int.Parse(statText.text) + 1).ToString();
                yield return new WaitForSeconds(animationTime/valueDifference);
            }
        }

        else if(valueDifference < 0)
        {
            for(int i = valueDifference; i < 0; i++)
            {
                statText.text = (int.Parse(statText.text) - 1).ToString();
                yield return new WaitForSeconds(animationTime/(valueDifference *-1));
            }
        }

        statText.text = statVariable.ToString();

        yield return null;
    }

    public void playerAttack(int attackCost, int attackDamage)
    {
        playerMana -= attackCost;
        StartCoroutine(UpdateStatText(playerManaText, playerMana));

        if(enemyShield > 0)
        {
            enemyShield -= attackDamage;
            if (enemyShield < 0)
                enemyShield = 0;

            StartCoroutine(UpdateStatText(enemyShieldText, enemyShield));
        }

        else
        {
            enemyHealth -= attackDamage;
            if (enemyHealth < 0)
                enemyHealth = 0;

            StartCoroutine(UpdateStatText(enemyHealthText, enemyHealth));
        }

        playerAtk1Button.interactable = false;
        playerAtk2Button.interactable = false;
        playerAtk3Button.interactable = false;

        enemyAttackController();
    }

    public void enemyAttackController()
    {
        if (enemyMana >= enemyAtk1Cost || enemyMana >= enemyAtk2Cost || enemyMana >= enemyAtk3Cost)
        {
            if (enemyMana >= enemyAtk1Cost && enemyMana >= enemyAtk2Cost && enemyMana >= enemyAtk3Cost)
            {
                int mostExpensiveAtkCost = Mathf.Max(enemyAtk1Cost, enemyAtk2Cost, enemyAtk3Cost);

                if (enemyAtk1Cost == mostExpensiveAtkCost)
                    enemyAttack(enemyAtk1Cost, enemyAtk1Damage);

                else if (enemyAtk2Cost == mostExpensiveAtkCost)
                    enemyAttack(enemyAtk2Cost, enemyAtk1Damage);

                else
                    enemyAttack(enemyAtk3Cost, enemyAtk3Damage);

            }

            else if (enemyMana >= enemyAtk1Cost && enemyMana >= enemyAtk2Cost)
            {
                int mostExpensiveAtkCost = Mathf.Max(enemyAtk1Cost, enemyAtk2Cost);

                if (mostExpensiveAtkCost == enemyAtk1Cost)
                    enemyAttack(enemyAtk1Cost, enemyAtk1Damage);

                else
                    enemyAttack(enemyAtk2Cost, enemyAtk2Damage);
            }

            else if (enemyMana >= enemyAtk1Cost && enemyMana >= enemyAtk3Cost)
            {
                int mostExpensiveAtkCost = Mathf.Max(enemyAtk1Cost, enemyAtk3Cost);

                if (mostExpensiveAtkCost == enemyAtk1Cost)
                    enemyAttack(enemyAtk1Cost, enemyAtk1Damage);

                else
                    enemyAttack(enemyAtk3Cost, enemyAtk3Damage);
            }

            else if (enemyMana >= enemyAtk2Cost && enemyMana >= enemyAtk3Cost)
            {
                int mostExpensiveAtkCost = Mathf.Max(enemyAtk2Cost, enemyAtk3Cost);

                if (mostExpensiveAtkCost == enemyAtk2Cost)
                    enemyAttack(enemyAtk2Cost, enemyAtk2Damage);

                else
                    enemyAttack(enemyAtk3Cost, enemyAtk3Damage);

            }

            else if (enemyMana >= enemyAtk1Cost)
                enemyAttack(enemyAtk1Cost, enemyAtk1Damage);

            else if (enemyMana >= enemyAtk2Cost)
                enemyAttack(enemyAtk2Cost, enemyAtk2Damage);

            else
                enemyAttack(enemyAtk3Cost, enemyAtk3Damage);
        }
    }

    public void enemyAttack(int attackCost, int attackDamage)
    {
        enemyMana -= attackCost;
        StartCoroutine(UpdateStatText(enemyManaText, enemyMana));

        if(playerShield > 0)
        {
            playerShield -= attackDamage;
            if (playerShield < 0)
                playerShield = 0;
            StartCoroutine(UpdateStatText(playerShieldText, playerShield));
        }

        else
        {
            playerHealth -= attackDamage;
            if (playerHealth < 0)
                playerHealth = 0;

            StartCoroutine(UpdateStatText(playerHealthText, playerHealth));
        }

    }
}

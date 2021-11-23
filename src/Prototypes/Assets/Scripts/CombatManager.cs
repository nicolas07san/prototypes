using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject enemyHand;

    [SerializeField] private TMP_Text diceText;
    [SerializeField] private TMP_Text roundText;

    [Header("Player Stats")]
    [SerializeField] private TMP_Text playerManaText;
    [SerializeField] private TMP_Text playerManaTextDifference;

    [SerializeField] private TMP_Text playerShieldText;
    [SerializeField] private TMP_Text playerShieldTextDifference;

    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text playerHealthTextDiffernce;

    [Header("Enemy Stats")]
    [SerializeField] private TMP_Text enemyManaText;
    [SerializeField] private TMP_Text enemyManaTextDifference;

    [SerializeField] private TMP_Text enemyShieldText;
    [SerializeField] private TMP_Text enemyShieldTextDifference;

    [SerializeField] private TMP_Text enemyHealthText;
    [SerializeField] private TMP_Text enemyHealthTextDifference;

    [Header("Screens Contol")]
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private Button passButton;

    [Header("Victory Count")]
    [SerializeField] private GameObject playerWinCount;
    [SerializeField] private GameObject enemyWinCount;

    private Vector3 playerHandPosition = new Vector3(-700, 0);
    private Vector3 enemyHandPosition = new Vector3(700, 0);

    private int diceNumber;
    private int round = 1;

    private Color32 decreaseColor = new Color32(226, 20, 20, 255);
    private Color32 increaseColor = new Color32(20, 226, 20, 255);

    // Player stats
    private int playerMana = 0;
    private int playerShield;
    private int playerHealth;

    private int playerWins = 0;

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

    private int enemyWins = 0;

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
                card.transform.localScale = new Vector3(1, 1);
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

    private IEnumerator GetInitialBasicStats()
    {
        diceText.text = 0.ToString();

        GameObject playerCard = playerHand.transform.GetChild(0).gameObject;

        GameObject playerAtk1 = playerCard.transform.Find("Attack1").gameObject;
        GameObject playerAtk2 = playerCard.transform.Find("Attack2").gameObject;
        GameObject playerAtk3 = playerCard.transform.Find("Attack3").gameObject;

        yield return null;

        playerShield = int.Parse(playerCard.transform.Find("Shield").GetComponent<TMP_Text>().text);
        playerShieldText.text = playerShield.ToString();

        playerHealth = int.Parse(playerCard.transform.Find("Health").GetComponent<TMP_Text>().text);
        playerHealthText.text = playerHealth.ToString();

        playerMana = 0;
        playerManaText.text = playerMana.ToString();

        yield return null;

        // Attack 1
        playerAtk1Button = playerAtk1.transform.Find("Button").GetComponent<Button>();
        playerAtk1Cost = int.Parse(playerAtk1.transform.Find("Cost").GetComponent<TMP_Text>().text);
        playerAtk1Damage = int.Parse(playerAtk1.transform.Find("Damage").GetComponent<TMP_Text>().text);

        playerAtk1Button.onClick.AddListener(delegate { playerAttack(playerAtk1Cost, playerAtk1Damage); });

        yield return null;

        // Attack 2
        playerAtk2Button = playerAtk2.transform.Find("Button").GetComponent<Button>();
        playerAtk2Cost = int.Parse(playerAtk2.transform.Find("Cost").GetComponent<TMP_Text>().text);
        playerAtk2Damage = int.Parse(playerAtk2.transform.Find("Damage").GetComponent<TMP_Text>().text);

        playerAtk2Button.onClick.AddListener(delegate { playerAttack(playerAtk2Cost, playerAtk2Damage); });

        yield return null;

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

        enemyMana = 0;
        enemyManaText.text = enemyMana.ToString();

        yield return null;

        // Attack 1
        enemyAtk1Cost = int.Parse(enemyAtk1.transform.Find("Cost").GetComponent<TMP_Text>().text);
        enemyAtk1Damage = int.Parse(enemyAtk1.transform.Find("Damage").GetComponent<TMP_Text>().text);

        yield return null;

        // Attack 2
        enemyAtk2Cost = int.Parse(enemyAtk2.transform.Find("Cost").GetComponent<TMP_Text>().text);
        enemyAtk2Damage = int.Parse(enemyAtk2.transform.Find("Damage").GetComponent<TMP_Text>().text);

        yield return null;

        // Attack 3
        enemyAtk3Cost = int.Parse(enemyAtk3.transform.Find("Cost").GetComponent<TMP_Text>().text);
        enemyAtk3Damage = int.Parse(enemyAtk3.transform.Find("Damage").GetComponent<TMP_Text>().text);

        yield return new WaitForSeconds(2f);

        StartCoroutine(StartRoundAnimation());

    }

    private IEnumerator StartRoundAnimation() {

        Vector3 startPosition = new Vector3(0, -200);
        Vector3 finalPosition = new Vector3(0, 200);

        roundText.gameObject.SetActive(true);
        roundText.transform.localPosition = startPosition;
        roundText.text = "Round " + round;
        

        while(Vector3.Distance(roundText.transform.localPosition, finalPosition) > 0.05f)
        {
            roundText.transform.localPosition = Vector3.Lerp(roundText.transform.localPosition, finalPosition, 0.03f);
            yield return null;
        }

        roundText.transform.localPosition = finalPosition;
        roundText.gameObject.SetActive(false);

        StartCoroutine(RollDice());

        yield return null;

    }

    private IEnumerator RollDice()
    {

        for (int i = 0; i < 100; i++)
        {
            diceNumber = Random.Range(1, 6);
            diceText.text = diceNumber.ToString();
            yield return new WaitForSecondsRealtime(3f / 100);
        }

        playerMana += diceNumber;
        StartCoroutine(UpdateStatText(playerManaText, playerManaTextDifference, playerMana));

        diceNumber = Random.Range(1, 6);

        enemyMana += diceNumber;
        StartCoroutine(UpdateStatText(enemyManaText, enemyManaTextDifference, enemyMana));

        yield return null;

        AttackController();
    }

    private void AttackController()
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
       
        passButton.gameObject.SetActive(true);
        
    }

    private IEnumerator UpdateStatText(TMP_Text statText, TMP_Text statTextDifference,int statVariable)
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
            statTextDifference.color = increaseColor;
            statTextDifference.text = "+" + valueDifference;
            statTextDifference.gameObject.SetActive(true);

            yield return null;

            for(int i = valueDifference; i > 0; i--)
            {
                statText.text = (int.Parse(statText.text) + 1).ToString();
                yield return new WaitForSeconds(animationTime/valueDifference);
            }
        }

        else if(valueDifference < 0)
        {
            statTextDifference.color = decreaseColor;
            statTextDifference.text = "" + valueDifference;
            statTextDifference.gameObject.SetActive(true);

            yield return null;

            for(int i = valueDifference; i < 0; i++)
            {
                statText.text = (int.Parse(statText.text) - 1).ToString();
                yield return new WaitForSeconds(animationTime/(valueDifference *-1));
            }
        }

        statText.text = statVariable.ToString();
        statTextDifference.gameObject.SetActive(false);

        yield return null;
    }

    private void playerAttack(int attackCost, int attackDamage)
    {
        playerMana -= attackCost;
        StartCoroutine(UpdateStatText(playerManaText, playerManaTextDifference, playerMana));

        if(enemyShield > 0)
        {
            enemyShield -= attackDamage;
            if (enemyShield < 0)
                enemyShield = 0;

            StartCoroutine(UpdateStatText(enemyShieldText, enemyShieldTextDifference, enemyShield));
        }

        else
        {
            enemyHealth -= attackDamage;
            if (enemyHealth < 0)
                enemyHealth = 0;

            StartCoroutine(UpdateStatText(enemyHealthText, enemyHealthTextDifference, enemyHealth));
        }

        playerAtk1Button.interactable = false;
        playerAtk2Button.interactable = false;
        playerAtk3Button.interactable = false;

        passButton.gameObject.SetActive(false);

        StartCoroutine(Pass());
    }

    private void enemyAttackController()
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

    private void enemyAttack(int attackCost, int attackDamage)
    {
        enemyMana -= attackCost;
        StartCoroutine(UpdateStatText(enemyManaText, enemyManaTextDifference, enemyMana));

        if(playerShield > 0)
        {
            playerShield -= attackDamage;
            if (playerShield < 0)
                playerShield = 0;
            StartCoroutine(UpdateStatText(playerShieldText, playerShieldTextDifference, playerShield));
        }

        else
        {
            playerHealth -= attackDamage;
            if (playerHealth < 0)
                playerHealth = 0;

            StartCoroutine(UpdateStatText(playerHealthText, playerHealthTextDiffernce, playerHealth));
        }

    }
    
    private IEnumerator Pass()
    {
        enemyAttackController();
        yield return new WaitForSeconds(2f);

        if (playerHealth <= 0 || enemyHealth <= 0)
        {
            round += 1;

            if (playerHealth <= 0)
            {
                enemyWins += 1;
                enemyWinCount.transform.GetChild(enemyWins-1).GetComponent<Image>().color = increaseColor;
            }
                
            else if (enemyHealth <= 0)
            {
                playerWins += 1;
                playerWinCount.transform.GetChild(playerWins - 1).GetComponent<Image>().color = increaseColor;
            }
                

            if(playerWins >= 2 || enemyWins >= 2)
            {

                yield return null;

                if(playerWins >= 2)
                {
                    victoryScreen.SetActive(true);
                }
                else if(enemyWins >= 2)
                {
                    gameoverScreen.SetActive(true);
                }

                Time.timeScale = 0f;
            }
            else
            {
                Destroy(playerHand.transform.GetChild(0).gameObject);
                Destroy(enemyHand.transform.GetChild(0).gameObject);

                yield return null;

                InitialCardPlacement(playerHand);
                yield return null;

                InitialCardPlacement(enemyHand);
                yield return null;

                StartCoroutine(GetInitialBasicStats());
            }
            
        }
        else
        {
            yield return null;

            StartCoroutine(RollDice());
        }
    }

    public void PassButton()
    {
        playerAtk1Button.interactable = false;
        playerAtk2Button.interactable = false;
        playerAtk3Button.interactable = false;
        StartCoroutine(Pass());
    }

}

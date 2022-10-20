using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    [Header("Announcement Text")]
    [SerializeField] private TMP_Text announcementText;
    [SerializeField] private Vector2 aTxtStartPos;
    [SerializeField] private Vector2 aTxtFinalPos;
    [SerializeField] private Image fadeImg;

    [Header("Backgrounds")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] backgrounds;

    [Header("Pass Button")]
    [SerializeField] private GameObject passButton;

    [Header("Dice")]
    [SerializeField] private Sprite[] diceSides;
    [SerializeField] private Image diceImage;

    [Header("Card Hands")]
    [SerializeField] GameObject playerHand;
    [SerializeField] GameObject enemyHand;

    [Header("Player Stats")]
    [SerializeField] private TMP_Text playerManaText;
    [SerializeField] private TMP_Text playerManaTextDifference;

    [SerializeField] private TMP_Text playerShieldText;
    [SerializeField] private TMP_Text playerShieldTextDifference;

    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text playerHealthTextDifference;

    [Header("Enemy Stats")]
    [SerializeField] private TMP_Text enemyManaText;
    [SerializeField] private TMP_Text enemyManaTextDifference;

    [SerializeField] private TMP_Text enemyShieldText;
    [SerializeField] private TMP_Text enemyShieldTextDifference;

    [SerializeField] private TMP_Text enemyHealthText;
    [SerializeField] private TMP_Text enemyHealthTextDifference;

    private Vector3 playerHandPosition = new Vector3(-700, 0);
    private Vector3 enemyHandPosition = new Vector3(700, 0);

    // Player stats
    private int playerMana = 0;
    private int playerShield;
    private int playerHealth;
    private int playerWins = 0;
    private bool playerWin = false;
    private bool playerTurn = false;
    private bool[] playerSpecialComboConfirm;
    private CardDisplay playerCardDisplay;
    private Card playerCard;

    // Enemy stats
    private int enemyMana = 0;
    private int enemyShield;
    private int enemyHealth;
    private int enemyWins = 0;
    private bool enemyWin = false;
    private bool enemyTurn = false;
    private bool[] enemySpecialComboConfirm;
    private Card enemyCard;
    private CardDisplay enemyCardDisplay;

    // Game stats
    private int round = 1;
    private int diceNumber;

    private void Awake() {
        Instance = this;

        if(LevelManager.isCampaignLevel)
        {
            backgroundImage.sprite = LevelManager.instance.level.levelImage;
        }
        else
        {   
            Sprite randomBg = backgrounds[UnityEngine.Random.Range(0, backgrounds.Length)];
            backgroundImage.sprite = randomBg;
        }
            
        
        for(int i = 0; i < 3; i ++)
        {
            Transform playerCard;
            Transform enemyCard;
            
            playerCard = LevelManager.instance.transform.GetChild(1).transform.GetChild(0);
            enemyCard =  LevelManager.instance.transform.GetChild(2).transform.GetChild(0);

            playerCard.SetParent(playerHand.transform);
            enemyCard.SetParent(enemyHand.transform);
        }

        Destroy(LevelManager.instance.transform.GetChild(1).gameObject);
        Destroy(LevelManager.instance.transform.GetChild(2).gameObject);
    
        
    }

    private void Start() {
       UpdateGameState(GameState.RoundStart); 
    }

    public void UpdateGameState(GameState newState){
        State = newState;

        switch(newState){
            case GameState.RoundStart:
                playerHand.transform.localPosition = playerHandPosition;
                enemyHand.transform.localPosition = enemyHandPosition;
                InitialCardPlacement(playerHand);
                InitialCardPlacement(enemyHand);
                GetInitialBasicStats();
                break;
            case GameState.PlayerTurn:
                playerTurn = true;
                TurnAnimation("Turno do Jogador");
                break;
            case GameState.EnemyTurn:
                enemyTurn = true;
                TurnAnimation("Turno do Inimigo");
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void InitialCardPlacement(GameObject cardHand){
        for(int i = 0; i < cardHand.transform.childCount; i++){
            GameObject card = cardHand.transform.GetChild(i).gameObject;
            switch(i){
                case 0:
                    card.transform.localScale = new Vector2(1, 1);
                    card.transform.localPosition = new Vector2(0, 60);
                    break;
                case 1:
                    card.transform.localScale = new Vector2(0.25f, 0.25f);
                    card.transform.localPosition = new Vector2(129, -350);
                    break;
                case 2:
                    card.transform.localScale = new Vector2(0.25f, 0.25f);
                    card.transform.localPosition = new Vector2(-129, -350);
                    break;
            }

            card.SetActive(true);
        } 
    }

    private void GetInitialBasicStats(){

        GameObject playerCardGO = playerHand.transform.GetChild(0).gameObject;
        playerCardDisplay = playerCardGO.GetComponent<CardDisplay>();
        playerCard = playerCardDisplay.Card;

        playerShield = playerCard.Shield;
        playerShieldText.text = playerShield.ToString();

        playerHealth = playerCard.Health;
        playerHealthText.text = playerHealth.ToString();

        playerMana = 0;
        playerManaText.text = playerMana.ToString();

        playerSpecialComboConfirm = new bool[playerCard.Combo.Length];

        // Light Attack 
        playerCardDisplay.LightAttackButton.onClick.AddListener(delegate { 
            PlayerAction(playerCard.LightAttackCost, playerCard.LightAttackDmg, Card.Action.LightAttack); });

        // Heavy Attack 
        playerCardDisplay.HeavyAttackButton.onClick.AddListener(delegate { 
            PlayerAction(playerCard.HeavyAttackCost, playerCard.HeavyAttackDmg, Card.Action.HeavyAttack); });

        // Support Action 
        playerCardDisplay.SupportActionButton.onClick.AddListener(delegate { 
            PlayerAction(playerCard.SupportActionCost, playerCard.SupportActionValue, Card.Action.SupportAction); });
        
        // TODO:Special Attack
            playerCardDisplay.SpecialAttackButton.onClick.AddListener(delegate{
              PlayerAction(0, playerCard.SpecialAttackDmg, Card.Action.SpecialAttack); });

        GameObject enemyCardGO = enemyHand.transform.GetChild(0).gameObject;
        enemyCardDisplay = enemyCardGO.GetComponent<CardDisplay>();
        enemyCard = enemyCardDisplay.Card;

        enemyShield = enemyCard.Shield;
        enemyShieldText.text = enemyShield.ToString();

        enemyHealth = enemyCard.Health;
        enemyHealthText.text = enemyHealth.ToString();

        enemyMana = 0;
        enemyManaText.text = enemyMana.ToString();

        enemySpecialComboConfirm = new bool[enemyCard.Combo.Length];

        StartRoundAnimation();

    }

    private void StartRoundAnimation(){
        //TODO: MOVE THIS TO A COIN FLIP ANIMATION + METHOD
        int coinSide = UnityEngine.Random.Range(0, 2);

        if(coinSide == 0)
            playerTurn = true;
        else
            enemyTurn = true;
        
        announcementText.transform.localPosition = aTxtStartPos;
        announcementText.gameObject.SetActive(true);
        fadeImg.gameObject.SetActive(true);
        announcementText.text = "Rodada " + round;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fadeImg.DOFade(0.5f, 0.25f));
        sequence.Append(announcementText.transform.DOLocalMoveX(-20f, 0.5f));
        sequence.Append(announcementText.transform.DOLocalMoveX(20f, 2f));
        sequence.Append(announcementText.transform.DOLocalMove(aTxtFinalPos, 0.5f).OnComplete(StartRound));
        DOTween.Play(sequence);
    }

    private IEnumerator RollDice(){
        int randomDiceSide = 0;
        for(int i = 0; i < 20; i++)
        {
            randomDiceSide = UnityEngine.Random.Range(0, 6);

            diceImage.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.1f);

        }

        yield return null;

        diceNumber = randomDiceSide + 1;

        if(playerTurn)
        {
            playerMana += diceNumber;
            StartCoroutine(UpdateStatText(playerManaText, playerManaTextDifference, playerMana));
            PlayerActionController();
        }

        else
        {
            enemyMana += diceNumber;
            StartCoroutine(UpdateStatText(enemyManaText, enemyManaTextDifference, enemyMana));
            EnemyActionController();
        }
    }

    private void TurnAnimation(string text){
        announcementText.transform.localPosition = aTxtStartPos;
        announcementText.gameObject.SetActive(true);
        fadeImg.gameObject.SetActive(true);
        announcementText.text = text;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fadeImg.DOFade(0.5f, 0.25f));
        sequence.Append(announcementText.transform.DOLocalMoveX(-20f, 0.5f));
        sequence.Append(announcementText.transform.DOLocalMoveX(20f, 2f));
        sequence.Append(announcementText.transform.DOLocalMove(aTxtFinalPos, 0.5f));
        sequence.Append(fadeImg.DOFade(0f, 0.25f).OnComplete(StartRollDice));
        DOTween.Play(sequence);

    }

    private void PlayerActionController()
    {

        if(playerMana >= playerCard.LightAttackCost)
            playerCardDisplay.LightAttackButton.interactable = true;

        if(playerMana >= playerCard.HeavyAttackCost)
            playerCardDisplay.HeavyAttackButton.interactable = true;

        if(playerMana >= playerCard.SupportActionCost)
            playerCardDisplay.SupportActionButton.interactable = true;
        
        if(IsSpecialAvailable(playerSpecialComboConfirm))
            playerCardDisplay.SpecialAttackButton.interactable = true;

        passButton.gameObject.SetActive(true);
    }

    private void PlayerAction(int actionCost, int actionValue, Card.Action actionType)
    {
        playerMana -= actionCost;
        StartCoroutine(UpdateStatText(playerManaText, playerManaTextDifference, playerMana));

        if(actionType == Card.Action.LightAttack)
        {
            ComboCheck(Card.Action.LightAttack, ref playerSpecialComboConfirm, playerCard.Combo);

            if(enemyShield > 0){
                enemyShield -= actionValue;
                if(enemyShield < 0)
                    enemyShield = 0;
                StartCoroutine(UpdateStatText(enemyShieldText, enemyShieldTextDifference, enemyShield));
            }
            else{
                enemyHealth -= actionValue;
                if(enemyHealth < 0)
                    enemyHealth = 0;
                StartCoroutine(UpdateStatText(enemyHealthText, enemyHealthTextDifference, enemyHealth));
            }

            // Play Damage  or Light Damage Sound
            // Play Damage or Light Damage Animation
        }

        else if(actionType == Card.Action.HeavyAttack)
        {
            ComboCheck(Card.Action.HeavyAttack, ref playerSpecialComboConfirm, playerCard.Combo);

            enemyShield -= actionValue;
            if(enemyShield < 0)
            {
                enemyHealth += enemyShield;
                enemyShield = 0;
                if(enemyHealth < 0)
                    enemyHealth = 0;
            }
            
            StartCoroutine(UpdateStatText(enemyShieldText, enemyShieldTextDifference, enemyShield));
            StartCoroutine(UpdateStatText(enemyHealthText, enemyHealthTextDifference, enemyHealth));
            // Play Damage or Heavy Damage Sound
            // Play Damage or Heavy Damage Animation
        }
        else if(actionType == Card.Action.SupportAction)
        {
            ComboCheck(Card.Action.SupportAction, ref playerSpecialComboConfirm, playerCard.Combo);

            if(playerCard.IsShield){
                playerShield += actionValue;
                StartCoroutine(UpdateStatText(playerShieldText, playerShieldTextDifference, playerShield));
                // Play Shield or Support Action Sound
                // Play Shield or Support Action Animation
                
            }
            else{
                playerHealth += actionValue;
                StartCoroutine(UpdateStatText(playerHealthText, playerHealthTextDifference, playerHealth));
                // Play Heal or Support Action Sound
                // Play Heal or Support Action Animation
            }
        }

        else if(actionType == Card.Action.SpecialAttack)
        {
            ComboCheck(Card.Action.SpecialAttack, ref playerSpecialComboConfirm, playerCard.Combo);

            //Special Attack Animation or Video
            enemyShield -= actionValue;
            if(enemyShield < 0)
            {
                enemyHealth += enemyShield;
                enemyShield = 0;
                if(enemyHealth <  0)
                    enemyHealth = 0;
            }
            StartCoroutine(UpdateStatText(enemyShieldText, enemyShieldTextDifference, enemyShield));
            StartCoroutine(UpdateStatText(enemyHealthText, enemyHealthTextDifference, enemyHealth));
            // Play Damage or Special Attack Sound
            // Play Damage or Special Attack Animation
        }

        playerCardDisplay.LightAttackButton.interactable = false;
        playerCardDisplay.HeavyAttackButton.interactable = false;
        playerCardDisplay.SupportActionButton.interactable = false;
        playerCardDisplay.SpecialAttackButton.interactable = false;

        passButton.SetActive(false);
        Invoke(nameof(Pass), 2f);
    }

    private void EnemyActionController()
    {
        if(UnityEngine.Random.Range(0, 100) < 10)
        {
            Array values = Enum.GetValues(typeof(Card.Action));
            Card.Action randomAction = (Card.Action)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            switch(randomAction)
            {
                case Card.Action.LightAttack:
                    if(enemyMana >= enemyCard.LightAttackCost)
                    {
                        EnemyAction(enemyCard.LightAttackCost, enemyCard.LightAttackDmg, Card.Action.LightAttack);
                        break;
                    }
                    goto default;

                case Card.Action.HeavyAttack:
                    if(enemyMana >= enemyCard.HeavyAttackCost)
                    {
                        EnemyAction(enemyCard.HeavyAttackCost, enemyCard.HeavyAttackDmg, Card.Action.HeavyAttack);
                        break;
                    }
                    goto default;

                case Card.Action.SupportAction:
                    if(enemyMana >= enemyCard.SupportActionCost)
                    {
                        EnemyAction(enemyCard.SupportActionCost, enemyCard.SupportActionValue, Card.Action.SupportAction);
                        break;
                    }
                    goto default;

                case Card.Action.SpecialAttack:                 
                    if(IsSpecialAvailable(enemySpecialComboConfirm))
                    {
                        EnemyAction(0, enemyCard.SpecialAttackDmg, Card.Action.SpecialAttack);
                        break;
                    }
                    goto default;
                
                default:
                    Pass();
                    break;
                        
            }
        }
        else if(IsSpecialAvailable(enemySpecialComboConfirm)){
            EnemyAction(0, enemyCard.SpecialAttackDmg, Card.Action.SpecialAttack);
        }
        else if(enemyMana >= enemyCard.LightAttackCost || enemyMana >= enemyCard.HeavyAttackCost || enemyMana >= enemyCard.SupportActionCost){
            for(int i = 0; i < enemySpecialComboConfirm.Length; i++)
            {
                if(!enemySpecialComboConfirm[i])
                {
                    switch(enemyCard.Combo[i]){
                        case(Card.Action.LightAttack):
                            if(enemyMana >= enemyCard.LightAttackCost)
                            {
                                EnemyAction(enemyCard.LightAttackCost, enemyCard.LightAttackDmg, Card.Action.LightAttack);
                                i = 100;
                            }
                            break;
                            
                        case(Card.Action.HeavyAttack):
                            if(enemyMana >= enemyCard.HeavyAttackCost)
                            {
                                EnemyAction(enemyCard.HeavyAttackCost, enemyCard.HeavyAttackDmg, Card.Action.HeavyAttack);
                                i = 100;
                            }
                            break;

                        case(Card.Action.SupportAction):
                            if(enemyMana >= enemyCard.SupportActionCost)
                            {
                                EnemyAction(enemyCard.SupportActionCost, enemyCard.SupportActionValue, Card.Action.SupportAction);
                                i = 100;
                            }
                            break;      
                    }
                }
            }
        }
        else
        {
            Pass();
        }
        
    }

    private void EnemyAction(int actionCost, int actionValue, Card.Action actionType)
    {
        enemyMana -= actionCost;
        StartCoroutine(UpdateStatText(enemyManaText, enemyManaTextDifference, enemyMana));

        if(actionType == Card.Action.LightAttack)
        {
            ComboCheck(Card.Action.LightAttack, ref enemySpecialComboConfirm, enemyCard.Combo);

            if(playerShield > 0){
                playerShield -= actionValue;
                if(playerShield < 0)
                    playerShield = 0;
                StartCoroutine(UpdateStatText(playerShieldText, playerShieldTextDifference, playerShield));
            }
            else{
                playerHealth -= actionValue;
                if(playerHealth < 0)
                    playerHealth = 0;
                StartCoroutine(UpdateStatText(playerHealthText, playerHealthTextDifference, playerHealth));
            }

            // Play Damage or Light Damage Sound
            // Play Damage or Light Damage Animation
        }

        else if(actionType == Card.Action.HeavyAttack)
        {
            ComboCheck(Card.Action.HeavyAttack, ref enemySpecialComboConfirm, enemyCard.Combo);

            playerShield -= actionValue;
            if(playerShield < 0)
            {
                playerHealth += playerShield;
                playerShield = 0;
            }
            StartCoroutine(UpdateStatText(playerShieldText, playerShieldTextDifference, playerShield));
            StartCoroutine(UpdateStatText(playerHealthText, playerHealthTextDifference, playerHealth));
            // Play Damage or Heavy Damage Sound
            // Play Damage or Heavy Damage Animation
        }

        else if(actionType == Card.Action.SupportAction)
        {
            ComboCheck(Card.Action.SupportAction, ref enemySpecialComboConfirm, enemyCard.Combo);

            if(enemyCard.IsShield){
                enemyShield += actionValue;
                StartCoroutine(UpdateStatText(enemyShieldText, enemyShieldTextDifference, enemyShield));
                // Play Shield or Support Action Sound
                // Play Shield or Support Action Animation
            }else{
                enemyHealth += actionValue;
                StartCoroutine(UpdateStatText(enemyHealthText, enemyHealthTextDifference, enemyHealth));
                // Play Heal or Support Action Sound
                // Play Heal or Support Action Animation
            }
        }
        else if(actionType == Card.Action.SpecialAttack)
        {
            ComboCheck(Card.Action.SpecialAttack, ref enemySpecialComboConfirm, enemyCard.Combo);
            //Special Attack Animation or Video

            playerShield -= actionValue;
            if(playerShield < 0)
            {
                playerHealth += playerShield;
                playerShield = 0;
                if(playerHealth < 0)
                    playerHealth = 0;
            }

            StartCoroutine(UpdateStatText(playerShieldText, playerShieldTextDifference, playerShield));
            StartCoroutine(UpdateStatText(playerHealthText, playerHealthTextDifference, playerHealth));
            // Play Damage or Special Attack Sound
            // Play Damage or Special Attack Animation
        }

        StartCoroutine(UpdateStatText(enemyManaText, enemyManaTextDifference, enemyMana));
        StartCoroutine(UpdateStatText(enemyShieldText, enemyShieldTextDifference, enemyShield));
        StartCoroutine(UpdateStatText(enemyHealthText, enemyHealthTextDifference, enemyHealth));

        Invoke(nameof(Pass), 2f);
    }

    public void Pass()
    {
        if(playerHealth <= 0 || enemyHealth <= 0)
        {
            
            if(playerHealth <= 0 && enemyHealth <= 0)
            {
                SuddenDeath();
            }
            else
            {
                round += 1;
                if(playerHealth <= 0)
                {
                    enemyWins += 1;
                    //Change Icon Color
                }
                else if(enemyHealth <= 0)
                {
                    playerWins += 1;
                    // Change Icon Color
                }

                if(playerWins >= 2 || enemyWins >= 2)
                {
                    //Stop Music
                    if(playerWins >= 2)
                    {
                        playerWin = true;
                        UpdateGameState(GameState.Victory);
                    }
                    else if(enemyWins >= 2)
                    {
                        UpdateGameState(GameState.Lose);
                    }
                }
                else
                {
                    Destroy(playerHand.transform.GetChild(0).gameObject);
                    Destroy(enemyHand.transform.GetChild(0).gameObject);
                    UpdateGameState(GameState.RoundStart);
                }
            }
        }
        else
        {
            if(playerTurn)
            {
                playerTurn = false;
                UpdateGameState(GameState.EnemyTurn);
            }
            else if(enemyTurn)
            {
                enemyTurn = false;
                UpdateGameState(GameState.PlayerTurn);
            }
        }
    }

    private void SuddenDeath()
    {
        // Sound Management

        playerHealth = 10;
        playerHealthText.text = playerHealth.ToString();

        playerShield = 10;
        playerShieldText.text = playerShield.ToString();

        enemyMana = 0;
        enemyManaText.text = enemyMana.ToString();

        enemyHealth = 10;
        enemyHealthText.text = enemyHealth.ToString();

        enemyShield = 10;
        enemyShieldText.text = enemyShield.ToString();

        enemyMana = 0;
        enemyManaText.text = enemyMana.ToString();

        // Announcement Text Animation
        announcementText.transform.position = aTxtStartPos;
        announcementText.gameObject.SetActive(true);
        announcementText.text = "Morte Súbita";
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(announcementText.transform.DOLocalMoveY(-20f, 0.5f));
        sequence.Append(announcementText.transform.DOLocalMoveY(20f, 2f));
        sequence.Append(announcementText.transform.DOLocalMove(aTxtFinalPos, 0.5f).OnComplete(StartRound));
        DOTween.Play(sequence);

        if(playerTurn)
        {
            playerTurn = false;
            UpdateGameState(GameState.EnemyTurn);
        }
        else if (enemyTurn)
        {
            enemyTurn = false;
            UpdateGameState(GameState.PlayerTurn);
        }

    }

    private void StartRound()
    {
        if(playerTurn)
            UpdateGameState(GameState.PlayerTurn);
        else
            UpdateGameState(GameState.EnemyTurn);
    }

    private void StartRollDice()
    {
        announcementText.gameObject.SetActive(false);
        fadeImg.gameObject.SetActive(false);
        StartCoroutine(RollDice());
    }

    private void ComboCheck(Card.Action actionType, ref bool[] specialComboConfirm, Card.Action[] combo)
    {
        // TODO: Update visual clues using specialComboConfirm
        if(actionType == Card.Action.SpecialAttack)
            for(int i = 0; i < specialComboConfirm.Length; i++)
                specialComboConfirm[i] = false;
        else
        {
            for(int i = 0; i < specialComboConfirm.Length; i++){
                // Current index is false
                if(!specialComboConfirm[i])
                {
                    if(actionType == combo[i]){
                        specialComboConfirm[i] = true;
                        break;
                    }
                    else
                    {
                        for(int j = 0; j < specialComboConfirm.Length; j++)
                            specialComboConfirm[j] = false;
                        break;
                    }
                }    
            }
        }
    }

    private bool IsSpecialAvailable(bool[] specialComboConfirm){
        int confirmCount = 0;
        for(int i = 0; i < specialComboConfirm.Length; i++)
            if(specialComboConfirm[i])
                confirmCount++;
        return confirmCount == specialComboConfirm.Length;
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
            statTextDifference.color = Color.green;
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
            statTextDifference.color = Color.red;
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

    public enum GameState {
        RoundStart,
        PlayerTurn,
        EnemyTurn,
        Victory,
        Lose
    }
}

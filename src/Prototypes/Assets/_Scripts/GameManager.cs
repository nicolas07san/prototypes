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

    // Player stats
    private int playerMana = 0;
    private int playerShield;
    private int playerHealth;
    private int playerWins = 0;
    private bool[] specialComboConfirm;
    private CardDisplay playerCardDisplay;
    private Card playerCard;

    // Enemy stats
    private int enemyMana = 0;
    private int enemyShield;
    private int enemyHealth;
    private int enemyWins = 0;
    private Card enemyCard;
    private CardDisplay enemyCardDisplay;

    // Game stats
    private int round = 1;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
       UpdateGameState(GameState.RoundStart); 
    }

    public void UpdateGameState(GameState newState){
        State = newState;

        switch(newState){
            case GameState.RoundStart:
                InitialCardPlacement(playerHand);
                InitialCardPlacement(enemyHand);
                GetInitialBasicStats();
                break;
            case GameState.PlayerTurn:
                TurnAnimation("Turno do Jogador");
                RollDice();
                break;
            case GameState.EnemyTurn:
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
                    card.transform.localScale = new Vector3(1, 1);
                    card.transform.localPosition = new Vector3(0, 200);
                    break;
                case 1:
                    card.transform.localScale = new Vector3(0.5f, 0.5f);
                    card.transform.localPosition = new Vector3(100, -300);
                    break;
                case 2:
                    card.transform.localScale = new Vector3(0.5f, 0.5f);
                    card.transform.localPosition = new Vector3(-100, -300);
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

        GameObject enemyCardGO = enemyHand.transform.GetChild(0).gameObject;
        enemyCardDisplay = enemyCardGO.GetComponent<CardDisplay>();
        enemyCard = enemyCardDisplay.Card;

        enemyShield = enemyCard.Shield;
        enemyShieldText.text = enemyShield.ToString();

        enemyHealth = enemyCard.Health;
        enemyHealthText.text = enemyHealth.ToString();

        enemyMana = 0;
        enemyManaText.text = enemyMana.ToString();

        // TODO:Special Attack

        // StartCoroutine(StartRoundAnimation());

    }

    private void StartRoundAnimation(){
        announcementText.transform.position = aTxtStartPos;
        announcementText.gameObject.SetActive(true);
        announcementText.text = "Rodada " + round;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(announcementText.transform.DOLocalMoveX(-20f, 0.5f));
        sequence.Append(announcementText.transform.DOLocalMoveX(20f, 2f));
        sequence.Append(announcementText.transform.DOLocalMove(aTxtFinalPos, 0.5f).OnComplete(StartRound));
        DOTween.Play(sequence);
    }

    private void RollDice(){

    }

    private void TurnAnimation(string text){

    }

    private void PlayerActionController()
    {

        if(playerMana >= playerCard.LightAttackCost)
            playerCardDisplay.LightAttackButton.interactable = true;

        if(playerMana >= playerCard.HeavyAttackCost)
            playerCardDisplay.HeavyAttackButton.interactable = true;

        if(playerMana >= playerCard.SupportActionCost)
            playerCardDisplay.SupportActionButton.interactable = true;
    }

    private void PlayerAction(int actionCost, int actionValue, Card.Action actionType)
    {
        playerMana -= actionCost;
        // Update Text Animation
        if(actionType == Card.Action.LightAttack)
        {
            if(enemyShield > 0){
                enemyShield -= actionValue;
                if(enemyShield < 0)
                    enemyShield = 0;
                // Update Text Animation
            }
            else{
                enemyHealth -= actionValue;
                if(enemyHealth < 0)
                    enemyHealth = 0;
                // Update Text Animation
            }

            // Play Damage  or Light Damage Sound
            // Play Damage or Light Damage Animation

        }

        else if(actionType == Card.Action.HeavyAttack)
        {
            enemyShield -= actionValue;
            if(enemyShield < 0)
            {
                enemyHealth -= enemyShield;
                enemyShield = 0;
            }
            // Update Text Animation
            // Play Damage or Heavy Damage Sound
            // Play Damage or Heavy Damage Animation
        }
        else if(actionType == Card.Action.SupportAction){
            if(playerCard.IsShield){
                playerShield += actionValue;
                // Update Text Animation
                // Play Shield or Support Action Sound
                // Play Shield or Support Action Animation
                
            }
            else{
                playerHealth += actionValue;
                // Update Text Animation
                // Play Heal or Support Action Sound
                // Play Heal or Support Action Animation
            }
        }

        playerCardDisplay.LightAttackButton.interactable = false;
        playerCardDisplay.HeavyAttackButton.interactable = false;
        playerCardDisplay.SupportActionButton.interactable = false;

        // Set Pass Button GameObejct Inactive
        // Start Method to Check Game Stats
    }

    private void EnemyAction(int acitonCost, int actionValue){

    }

    private void StartRound(){
        UpdateGameState(GameState.PlayerTurn);
    }

    public enum GameState {
        RoundStart,
        PlayerTurn,
        EnemyTurn,
        Victory,
        Lose
    }
}
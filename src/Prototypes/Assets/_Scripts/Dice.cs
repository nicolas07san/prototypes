﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    [SerializeField] private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private Image rend;
    public int finalSide;

	// Use this for initialization
	private void Start () {

        // Assign Renderer component
        rend = GetComponent<Image>();

        Roll();
        Debug.Log(finalSide);
	}

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.1f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        
    }

    public void Roll(){
        StartCoroutine(nameof(RollTheDice));
    }
}

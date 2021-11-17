using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private int randomNumber;

    private int minNumber = 1;
    private int maxNumber = 6;

    private float rollTime = 3f;
    private int numberOfRolls = 100;

    [SerializeField] private TMPro.TMP_Text numberText;

    void Start()
    {
        StartCoroutine(RollDice());
    }

    public IEnumerator RollDice()
    {
        for(int i = 0; i < numberOfRolls; i++)
        {
            randomNumber = Random.Range(minNumber, maxNumber);
            numberText.text = randomNumber.ToString();
            yield return new WaitForSecondsRealtime(rollTime/numberOfRolls);
        }
    }

    public int RandomNumber
    {
        get
        {
            return this.randomNumber;
        }
    }
}

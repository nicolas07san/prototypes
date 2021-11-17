using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private int randomNumber;

    public int minNumber = 1;
    public int maxNumber = 6;

    private float currentTime;

    public float finalTime = 5f;

    public Text numberText;

    void Start()
    {
        currentTime = finalTime;
    }
    

    void Update()
    {
        currentTime -= 1f * Time.deltaTime;

        if(currentTime >= 0)
        {
            randomNumber = Random.Range(minNumber, maxNumber);
            numberText.text = randomNumber.ToString();
            
        }
        
    }
}

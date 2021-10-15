using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceV2 : MonoBehaviour
{
    private int randomNumber;

    public int minNumber = 1;
    public int maxNumber = 6;

    private float currentTime;
    private float pauseTime = 0.2f;

    public float finalTime = 5f;

    public Text numberText;



    void Start()
    {
        currentTime = finalTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= pauseTime * Time.deltaTime;

        if (currentTime >= 0)
        {
            randomNumber = Random.Range(minNumber, maxNumber);
            numberText.text = randomNumber.ToString();

        }

    }

}

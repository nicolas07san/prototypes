using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private int minNumber = 1;
    private int maxNumber = 6;

    private float currentTime;
    private float lastTime;

    public float totalTimer = 5f;
    public float pauseTime = 1f;

    public Text numberText;

    private int randomNumber;

    void Start()
    {
        currentTime = totalTimer;
        lastTime = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= pauseTime * Time.deltaTime;
        Debug.Log(currentTime);

        if(currentTime <= lastTime - pauseTime && currentTime >= 0)
        {
            randomNumber = Random.Range(minNumber, maxNumber);
            numberText.text = randomNumber.ToString();
            lastTime = currentTime;
            pauseTime += 1f;
            
        }
        

    }
}

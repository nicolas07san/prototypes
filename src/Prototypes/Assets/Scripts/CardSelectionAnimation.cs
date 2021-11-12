using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionAnimation : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    private float animationSpeed = 0.1f;

    private void Awake()
    {
        initialPosition = transform.position;
        finalPosition = transform.parent.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, animationSpeed);
    }

    private void OnDisable()
    {
        transform.position = initialPosition;
    }
}

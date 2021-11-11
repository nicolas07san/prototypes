using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionAnimation : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField]private Vector3 finalPosition;

    [SerializeField] private float animationSpeed;

    private void Awake()
    {
        initialPosition = transform.position;
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

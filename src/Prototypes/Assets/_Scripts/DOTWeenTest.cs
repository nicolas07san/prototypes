using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTWeenTest : MonoBehaviour
{
    [SerializeField] private float _animationTime;
    [SerializeField] private Transform _destination;

    void Start()
    {
        transform.DOMove(_destination.transform.position, _animationTime).SetEase(Ease.InElastic);
    }
}

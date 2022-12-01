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
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveX(-20f, 0.5f));
        sequence.Append(transform.DOLocalMoveX(20f, 2f));
        sequence.Append(transform.DOLocalMoveX(1476f, 0.5f).OnComplete(Completed));
        DOTween.Play(sequence);
        
    }

    private void Completed(){
        Debug.Log("Tween Finished");
    }

}

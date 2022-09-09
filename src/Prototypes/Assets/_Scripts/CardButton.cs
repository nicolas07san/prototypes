using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    [SerializeField]private float _scaleUpValue = 1.4f;
    [SerializeField]private float _animationTime = 0.25f;
    private Button _button;

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_button.interactable)
        {
            ScaleUp();
            transform.SetAsLastSibling();
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleDown();
    }

    private void ScaleUp()
    {
        transform.DOScale(new Vector3(_scaleUpValue, _scaleUpValue, _scaleUpValue), _animationTime);
    }

    private void ScaleDown()
    {
        transform.DOScale(Vector3.one, _animationTime);
    }


}

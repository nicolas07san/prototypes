using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   

    [SerializeField] private string _headerText;
    [SerializeField, TextArea] private string _contentText;

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ScaleUp();
        transform.SetAsLastSibling();
        TooltipSystem.Show(_contentText, _headerText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleDown();
        TooltipSystem.Hide();
    }

    private void ScaleUp()
    {
        transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.4f);
    }

    private void ScaleDown()
    {
        transform.DOScale(Vector3.one, 0.4f);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem _instance;

    public Tooltip tooltip;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    public static void Show(string content, string header)
    {
        _instance.tooltip.SetText(content, header);
        _instance.tooltip.gameObject.SetActive(true);
        _instance.StartCoroutine(_instance.ActiveTooltip());
    }

    public static void Hide()
    {
        _instance.tooltip.gameObject.SetActive(false);
        _instance.tooltip.SetAlpha(0f);
        _instance.StopCoroutine(_instance.ActiveTooltip());
    }

    public IEnumerator ActiveTooltip()
    {
        yield return new WaitForSeconds(.5f);
        _instance.tooltip.SetAlpha(255f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _headerField;
    [SerializeField] private TextMeshProUGUI _contentField;

    [SerializeField] private LayoutElement _layoutElement;

    [SerializeField] private int characterWrapLimit;

    private RectTransform _rectTransform;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        SetAlpha(0f);
    }

    public void SetText(string content, string header)
    {
        if (string.IsNullOrEmpty(header))
        {
            _headerField.gameObject.SetActive(false);
        }
        else
        {
            _headerField.gameObject.SetActive(true);
            _headerField.text = header;
        }

        _contentField.text = content;

        int headerLength = _headerField.text.Length;
        int contentLength = _contentField.text.Length;

        _layoutElement.enabled = headerLength > characterWrapLimit || contentLength > characterWrapLimit;
    }

    void Update()
    {

        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        _rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = mousePosition;
    }

    public void SetAlpha(float alpha)
    {
        GetComponent<CanvasRenderer>().SetAlpha(alpha);
        _headerField.GetComponent<CanvasRenderer>().SetAlpha(alpha);
        _contentField.GetComponent<CanvasRenderer>().SetAlpha(alpha);
    }
        
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 toolTipLocalPos;
    public GameObject toolTip;
    public ItemData ItemData { get; set; }

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.SetActive(true);
        SetToolTipPosition();
        SetToolTipText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.GetComponent<Text>().text = null;
        toolTip.SetActive(false);
    }

    private void SetToolTipPosition()
    {
        RectTransform toolTipRectTransform = toolTip.GetComponent<RectTransform>();
        //toolTipRectTransform.anchoredPosition = rectTransform.anchoredPosition;
        toolTipRectTransform.localPosition = toolTipLocalPos;     
    }

    private void SetToolTipText()
    {
        Text toolTipText = toolTip.GetComponent<Text>();
        toolTipText.text = ItemData.GetToolTipInfo();
    }
}

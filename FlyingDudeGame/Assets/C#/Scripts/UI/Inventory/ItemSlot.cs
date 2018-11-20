using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backGround;
    public Image selected;
    public Image icon;

    public float xOffset;
    public float yOffset;

    private Vector3 toolTipLocalPos;
    public GameObject toolTip;
    private ItemData itemData;

    private RectTransform rectTransform;

    public ItemData ItemData { get { return itemData; } set
        {
            DeSelect();
            if (value == null)
            {
                icon.gameObject.SetActive(false);
                return;
            }
                
            itemData = value;
            icon.gameObject.SetActive(true);
            icon.sprite = value.icon;
        }
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector3 v = rectTransform.localPosition;
        toolTipLocalPos = new Vector3(v.x + xOffset, v.y + yOffset, v.z);
    }

    private void Update()
    {
        Vector3 v = rectTransform.localPosition;
        toolTipLocalPos = new Vector3(v.x + xOffset, v.y + yOffset, v.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip();
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DeSelect();
        toolTip.GetComponentInChildren<Text>().text = "";
        toolTip.SetActive(false);
    }

    public void Select()
    {
        selected.gameObject.SetActive(true);
    }

    public void DeSelect()
    {
        selected.gameObject.SetActive(false);
    }

    private void ShowToolTip()
    {
        toolTip.SetActive(true);
        SetToolTipPosition();
        SetToolTipText();
    }

    private void SetToolTipPosition()
    {

        RectTransform toolTipRectTransform = toolTip.GetComponent<RectTransform>();

        //toolTipRectTransform.localScale = Vector3.one;
        //toolTipRectTransform.offsetMin = new Vector2(25, 25);
        //toolTipRectTransform.offsetMax = new Vector2(25, 25);
        //toolTipRectTransform.localRotation = Quaternion.Euler(0, 0, 0);

        //toolTipRectTransform.anchoredPosition = rectTransform.anchoredPosition;

        toolTipRectTransform.sizeDelta = new Vector2(200,200);
        toolTipRectTransform.localPosition = toolTipLocalPos;
    }

    private void SetToolTipText()
    {
        Text toolTipText = toolTip.GetComponentInChildren<Text>();
        toolTipText.text =(itemData == null)?"YOU HAVE NOTHING": itemData.GetToolTipInfo();
    }
}

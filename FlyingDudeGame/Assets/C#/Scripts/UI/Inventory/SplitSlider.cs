using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplitSlider : MonoBehaviour
{
    [SerializeField]
    private Text valueText;
    [SerializeField]
    private Slider slider;
    private GameObject parent;
    private RectTransform parentRectTransform;

    public Vector3 offset;
    private int slotIndex;

    private InventoryContainer inventoryContainer;
    // Use this for initialization
    void Start()
    {
        parent = gameObject;
        parentRectTransform = parent.GetComponent<RectTransform>();
        inventoryContainer = GetComponentInParent<InventoryWindow>().InventoryContainer;
        slider.wholeNumbers = true;
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inventoryContainer.OnSpliderAccept((int)slider.value, slotIndex);
        }else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DisableSlider();
        }
    }

    public void EnableSlider(RectTransform rectTransform, int maxAmt, int slotIndex)
    {
        parent.SetActive(true);
        slider.maxValue = maxAmt;
        parentRectTransform.localPosition = rectTransform.localPosition + offset;
        this.slotIndex = slotIndex;
        valueText.text = "0";
    }

    public void DisableSlider()
    {
        parent.SetActive(false);
    }

    public void OnValueChange()
    {
        valueText.text = slider.value.ToString();
    }
}

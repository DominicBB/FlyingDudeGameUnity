using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CursorItem : MonoBehaviour
{
    public static bool IsItemOnCursor { get; private set; }
    public ItemStack<Item> itemOnCursor;

    public int ItemOrigen { get; private set; }

    [SerializeField]
    private RectTransform iconRect;
    public Image icon;

    public void PutItemOnCursor(ItemStack<Item> itemStack, int index)
    {
        ItemOrigen = index;
        iconRect.gameObject.SetActive(true);
        itemOnCursor = itemStack;
        icon.sprite = itemStack.Peek().itemData.icon;
        icon.GetComponentInChildren<Text>().text = itemStack.StackSize.ToString();
        IsItemOnCursor = true;
    }

    public void RemoveItem()
    {
        ItemOrigen = -1;
        iconRect.gameObject.SetActive(false);
        IsItemOnCursor = false;
        itemOnCursor = null;
        icon.sprite = null;
        iconRect.GetComponentInChildren<Text>().text = "0";
    }

    private float cooldown = 1;
    private float lastTime;
    public void UpdateCursorItemPosition(Vector2 position)
    {
        //if (Time.time > lastTime + cooldown)  
        //{
        //    lastTime = Time.time;

        //    Debug.Log("Cursor: " + position);
        //    Debug.Log(parentRect.localPosition);
        //}
        //Debug.Log("Width " + Screen.width);
        //Debug.Log("Height " + Screen.height);

        iconRect.localPosition = ScreenSpaceToCanvasSpace(position);
    }

    private Vector2 ScreenSpaceToCanvasSpace(Vector2 vector2)
    {
        float res = ((float)Screen.currentResolution.width / (float)Screen.currentResolution.height);

        Vector2 v = new Vector2
        {
            x = vector2.x * res,
            y = vector2.y * res
        };
        return v;
    }
}

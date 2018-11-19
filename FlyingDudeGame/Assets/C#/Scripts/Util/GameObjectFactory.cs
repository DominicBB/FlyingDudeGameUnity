using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class GameObjectFactory
{

   public static GameObject CreateText(Transform parent, string text, float x, float y, Font font, int fontSize, string name)
    {
        GameObject newGameObject = new GameObject(name);

        newGameObject.transform.SetParent(parent);

        RectTransform rectTransform = newGameObject.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(x, y, 0);
        rectTransform.localPosition = new Vector3(x, y, 0);
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.localRotation = Quaternion.Euler(0, 0, 0);

        Text textComp = newGameObject.AddComponent<Text>();
        textComp.text = text;
        textComp.font = font;
        textComp.fontSize = fontSize;

        return newGameObject;
    }
}

using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Skillicon : MonoBehaviour {
    public Image selectionSprite;

    public void Select()
    {

        selectionSprite.enabled = true;
    }

    public void DeSelect()
    {
        selectionSprite.enabled = false; 
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EscMenu : MonoBehaviour
{
    public GameObject menuPanel;
    private bool menuOpen;
    private bool flyTextOnScreen;
    public Font flyTextFont;
    private string NoImplmentationString = "Nothing here";

    private void Start()
    {
        menuPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEsc();
        }
    }

    public void OnEsc()
    {
        if (menuOpen)
        {
            OnResume();
        }
        else
        {
            OnOpen();
        }
    }

    public void OnOpen()
    {
        menuOpen = true;
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuPanel.gameObject.SetActive(true);
    }

    public void OnResume()
    {
        menuOpen = false;
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.gameObject.SetActive(false);
    }

    public void OnOptions()
    {
        if (!flyTextOnScreen)
        {
            StartCoroutine(NoImplmentationFlyText());
        }
    }

    public void OnExit()
    {
        Application.Quit();
    }

    private IEnumerator NoImplmentationFlyText()
    {
        flyTextOnScreen = true;

        GameObject textObj = GameObjectFactory.CreateText(transform, NoImplmentationString, 0, 0, flyTextFont, 20, "NoImplText");
        textObj.transform.SetAsLastSibling();

        Text text = textObj.GetComponent<Text>();
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.red;

        yield return new WaitForSecondsRealtime(.5f);

        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            Color c = text.color;
            c.a = alpha;
            text.color = c;
            yield return new WaitForSecondsRealtime(.1f);
        }
        Destroy(textObj);
        flyTextOnScreen = false;
    }
}

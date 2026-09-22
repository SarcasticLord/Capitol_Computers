using System.Collections;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject taskbarIcon;
    public GameObject[] revealTexts;

    void Start()
    {
        ResetText();
    }

    public void Open()
    {
        gameObject.SetActive(true);

        if (taskbarIcon != null)
            taskbarIcon.SetActive(true);

        ResetText();
        StartCoroutine(RevealText());
    }

    public void Close()
    {
        gameObject.SetActive(false);

        if (taskbarIcon != null)
            taskbarIcon.SetActive(false);

        ResetText();
    }

    void ResetText()
    {
        foreach (GameObject loadText in revealTexts)
        {
            loadText.SetActive(false);
        }
    }

    IEnumerator RevealText()
    {
        foreach (GameObject loadText in revealTexts)
        {
            loadText.SetActive(true);
            yield return new WaitForSeconds(.05f);
        }
    }
}
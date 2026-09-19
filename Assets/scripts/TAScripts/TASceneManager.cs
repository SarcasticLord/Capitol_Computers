using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TASceneManager : MonoBehaviour
{

    public GameObject statsWindow;
    public GameObject[] StatsText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetText(StatsText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CapitolWindows() // OPENS WINDOWS OS
    {
        SceneManager.LoadScene("CapitolWindows");
    }

    IEnumerator WindowText(GameObject[] textArray) 
    {
        foreach (GameObject loadText in textArray) 
        {
            loadText.SetActive(true);
            yield return new WaitForSeconds(.05f);
        }

    }

    void ResetText(GameObject[] textArray) // keeps the text hidden when the ui isnt active
    {
        foreach (GameObject loadText in textArray)
        {
            loadText.SetActive(false);
        }
    }

    public void OpenStatsWindow() // OPENS stats  WINDOW 
    {
        
        statsWindow.SetActive(true);

        ResetText(StatsText);
        StartCoroutine(WindowText(StatsText));
    }

    public void CloseStatsWindow() // close stats window
    {
        
        statsWindow.SetActive(false);

        ResetText(StatsText);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class OSLoadingController : MonoBehaviour
{
    public ScrollRect scrollRect; // controls how cmd scrolls
    public Transform content;
    public GameObject TerminalLines;

    public float delay = 0.4f;

    public string[] lines;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PrintLines());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrintLines()
    {
        foreach (string line in lines)
        {
            AddLine(line);

            yield return new WaitForSeconds(delay);
        }
        SceneManager.LoadScene("CapitolWindows");
    }


    void AddLine(string text)
    {
        GameObject line = Instantiate(TerminalLines, content);
        line.GetComponent<TMP_Text>().text = text;


        scrollRect.verticalNormalizedPosition = 0f;
    }


    // public void Updateoutput(string msg)
    // {
    //     if (!Application.isPlaying) return;
    //     output += "\n" + msg;
    //     outputText.text = output;
    //     if (isActiveAndEnabled)
    //         StartCoroutine("ScrollToBottom");
        
    // }
}

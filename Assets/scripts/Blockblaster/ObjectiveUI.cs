// this was in game manager but i seperated them to make the game manager actually work like its supposed too
// this is the ui and score parts of the objectives thing

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Analytics;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public static ObjectiveUI instance = null;

    public TextMeshProUGUI stockedText;
    public TextMeshProUGUI netflixText;
    public TextMeshProUGUI trashText;

    public GameObject winTextObject;
   
    public Timer timer;
    void Start()
    {
        winTextObject.SetActive(false);
    }

   
    void Update()
    {
        
        stockedText.text = "Stock the shelves: " + GameManager.instance.stockCount.ToString() + "/10";
        netflixText.text = "Metflicks employees stopped: " + GameManager.instance.netflixCount.ToString();
        trashText.text = "Trash picked up: " + GameManager.instance.trashCount.ToString() + "/20";

    }

 }
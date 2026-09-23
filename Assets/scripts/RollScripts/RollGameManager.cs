// this is the game manager for RollaGoose

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;

public class RollGameManager : MonoBehaviour
{

    public GameObject winTextObject;
    //public GameObject loseTextObject;
    public Timer timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

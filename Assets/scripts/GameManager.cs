using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int lives = 3;

   public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("dont destroy");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {        
        ResetScores();
    }

    public int stockCount;
    public int netflixCount;
    public int trashCount;

    public void Objectives() // when all three conditions are met stop the timer, end the game, and send the player back to title
    {
        if (stockCount >= 10 && netflixCount >= 50 && trashCount >= 20) 
        {
            
            ObjectiveUI.instance.winTextObject.SetActive(true);
            SceneManagerScript.instance.Invoke("ToTitle", 5f);

            if (ObjectiveUI.instance.timer != null)
            {
                ObjectiveUI.instance.timer.StopTimer();
            }
            ResetScores();
        
        }
        
    }
    
// for more objective stuff find the Objectives UI script
    public void StockObjective() // stocking the shelves
    {
        Objectives();
    }

    public void MetflicksObjective() // killing the metflicks employees
    {
       Objectives();
    }

    public void TrashObjective() // sweeping up the trash
    {
        Objectives();
    }

    public void ResetScores()
    {
        stockCount = 0;
        netflixCount = 0;
        trashCount = 0;
        
    }

    

    public void DecreaseLives()
    {
        lives--;
    }

    public int GetLives()
    {
        return lives;
    }

    public void ResetGame()
    {
        lives = 3;
    }

    
}

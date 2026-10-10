using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;

public class TAGameManager : MonoBehaviour
{
    public static TAGameManager instance;
    
    public List<string> inventory = new List<string>();

    [HideInInspector] public string terminalLog = "";

    string SavePath{
        get { return Path.Combine(FilePaths.GetCapitolFolder(), "Terminal.json"); }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // returns true if a saved folder was restored (which also prints its description)
    public bool Load()
    {
        if (!File.Exists(SavePath)) return false;
 
        try
        {
            string json = File.ReadAllText(SavePath);
            SaveState gameState = JsonUtility.FromJson<SaveState>(json);
 
            inventory = gameState.inventory;
 
            FolderRoom afolderRoom = TANavigationManager.instance.GetFolderByName(gameState.currentFolder);
            if (afolderRoom != null)
            {
                TANavigationManager.instance.SwitchFolders(afolderRoom);
                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("bad terminal save: " + e.Message);
        }
 
        return false;
    }

    public void Save()
    {
        try
        {
            SaveState gameState = new SaveState();
            gameState.currentFolder = TANavigationManager.instance.currentFolder.name;
            gameState.inventory = inventory;

            Directory.CreateDirectory(FilePaths.GetCapitolFolder());
            File.WriteAllText(SavePath, JsonUtility.ToJson(gameState, true));
            Debug.Log("game saved - from Save()");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Couldn't save terminal: " + e.Message);
        }
    }

    public void ResetGame()
    {
        inventory.Clear();
    }
}

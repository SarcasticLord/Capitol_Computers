using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;

public class TAGameManager : MonoBehaviour
{
    public static TAGameManager instance;
    
    public List<string> inventory = new List<string>();

    string SavePath{
        get { return Path.Combine(FilePaths.GetCapitolFolder(), "Terminal.json"); }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
        TANavigationManager.instance.onRestart += ResetGame; // notice no () its not calling it its pointing to it
        
        

    }

    public void Load()
    {
        if (!File.Exists(SavePath)) return;
        {
            try {
            string json = File.ReadAllText(SavePath);
            SaveState gameState = JsonUtility.FromJson<SaveState>(json);
            FolderRoom afolderRoom = TANavigationManager.instance.GetFolderByName(gameState.currentFolder);
            if (afolderRoom != null)
                TANavigationManager.instance.SwitchFolders(afolderRoom);

            inventory = gameState.inventory;
            }

            catch (System.Exception e)
            {
                Debug.LogWarning("bad terminal save: ");
            }
        }
        
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

    void ResetGame()
    {
        inventory.Clear();
    }
}

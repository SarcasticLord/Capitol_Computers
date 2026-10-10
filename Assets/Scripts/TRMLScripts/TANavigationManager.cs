using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TANavigationManager : MonoBehaviour

{
    public static TANavigationManager instance;

    public FolderRoom startingFolder;
    public FolderRoom currentFolder;

    public delegate void Restart();
    public event Restart onRestart;

    public Exit toKeyNorth;
    public List<FolderRoom> folders;



    private Dictionary<string, FolderRoom> exitFolder = new Dictionary<string, FolderRoom>();

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        currentFolder = startingFolder;
        Unpack();
        TAGameManager.instance.Load();
    }

    void Unpack()
    {
        string description = currentFolder.Description;

        exitFolder.Clear();
        foreach (Exit e in currentFolder.exits)
        {
            if (!e.isHidden)
            {
                description += " " + e.description;
                exitFolder.Add(e.folderName.ToString(), e.folderRoom);
            }
        }

        TAInputManager.instance.UpdateTerminal(description);

        if (currentFolder.name == "notifications")
        {
            SceneManager.LoadScene(2); // opens the windows scene
        }

    }

    public void GameRestart()
    {
        onRestart.Invoke();             // calling the restsrt event
        currentFolder = startingFolder;     // puts the player back at the start
        toKeyNorth.isHidden = true;
       
        bool isFound = false;
        

        Unpack();
    }
    public void SwitchFolders(FolderRoom folderRoom)
    {
        currentFolder = folderRoom;
        Unpack();
    }
    public bool SwitchFolders(string input)
    {
        input = input.ToLower();

        if (exitFolder.ContainsKey(input))
        {
            Exit exit = getExit(input);

            if (TAGameManager.instance.inventory.Contains("pubKey") || !exit.isLocked)
            {
                currentFolder = exitFolder[input];
                TAInputManager.instance.UpdateTerminal("\nOpening " + input);
                Unpack();
                return true;
            }
            else
                return false;


        }
        return false;

    }
    Exit getExit(string name)
    {
        foreach (Exit e in currentFolder.exits)
        {
            if (e.folderName.ToString() == name.ToLower())
                return e;
            
        }
        return null;
    }

    public bool getItem(string item)

    {
        bool isFound = false;
        foreach (string i in currentFolder.items)
        {
            if (i == item)
            {
                isFound = true;
                if(item == "systemData")
                {
                    
                    toKeyNorth.isHidden = false;
                    TAInputManager.instance.UpdateTerminal("Downloading system data...");
                }

                if(item == "eventViewer")
                {
                    
                    TAInputManager.instance.UpdateTerminal("Downloading Event Viewer logs...");
                }

                if(item == "pubkey")
                {
                    
                    TAInputManager.instance.UpdateTerminal("Downloading Public Key!!!");
                }
            }
            
        }
        if (isFound)
        {
            currentFolder.items.Remove(item);
            currentFolder.Description = "You already downloaded these folders.";
        }
        return isFound;

    }
    public FolderRoom GetFolderByName(string name)
    {
        foreach(FolderRoom afolder in folders)
            if (afolder.name == name)
                return afolder;
        return null;
    }
    
}
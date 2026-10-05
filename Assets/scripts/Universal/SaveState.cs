// this is for saving and coming back to games and picking up where you left off
// this script also can save your last visited scene and use that for stuff

using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class SaveState
{

    // saves last scene
    public static string lastScene = "";
    
    // terminal saves 

    public string currentFolder;
    public List<string> inventory;
}

    



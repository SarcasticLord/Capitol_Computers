using UnityEngine;
using System.Collections.Generic;


[System.Serializable]

public class SaveState
{

    // saves last scene
    public static string lastScene = "";


    // BlockBLaster saves

    public string rollTime;
    public string blockblasterTime;
    public int stockCount;
    public int netflixCount;
    public int trashCount; 


    // terminal saves 

    public string currentFolder;
    public List<string> inventory;
}

    



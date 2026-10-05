using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "room", menuName = "RoomsExits/room")]
public class FolderRoom : ScriptableObject
{
    public string folderName;
    [TextArea]
    public string Description;
    public Exit[] exits;

    //public bool hasKey;
    //public bool hasOrb;
  

    public List <string> items;
}

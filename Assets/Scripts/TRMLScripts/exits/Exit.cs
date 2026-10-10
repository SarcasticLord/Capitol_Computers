using UnityEditor.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "exit", menuName = "RoomsExits/exit")]
public class Exit : ScriptableObject
{
    public string folderName;
    [TextArea]
    public string description;
    public FolderRoom folderRoom; // the room this exit will be attached to

    public bool isLocked;
    public bool isHidden;
}

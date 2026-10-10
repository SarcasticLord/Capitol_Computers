using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public static class FilePaths
{
    static string GetGameFolder()
    {
#if UNITY_STANDALONE_OSX
        // dataPath is Game.app/Contents, so go up twice to land beside the .app
        return Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName;
#else
        // Windows/Linux: dataPath is Game_Data, parent is the folder with the exe
        return Directory.GetParent(Application.dataPath).FullName;
#endif
    }

    public static string GetCapitolFolder()
    {
        return Path.GetFullPath(Path.Combine(GetGameFolder(), "CapitolSystems"));
    }

    public static void WriteMessage(string fileName, string text)
    {
        try
        {
            string folder = GetCapitolFolder();
            Directory.CreateDirectory(folder);
            File.WriteAllText(Path.Combine(folder, fileName), text);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogWarning("Couldn't write message file: " + e.Message);
        }
    }

    public static void OpenCapitolFolder()
    {
        string folder = GetCapitolFolder();
        Directory.CreateDirectory(folder);

        try
        {
#if UNITY_STANDALONE_WIN
            Process.Start("explorer.exe", "\"" + folder + "\"");
#elif UNITY_STANDALONE_OSX
            Process.Start("open", "\"" + folder + "\"");
#else
            Process.Start("xdg-open", "\"" + folder + "\"");
#endif
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogWarning("Couldn't open folder: " + e.Message);
        }
    }

    // DDLC style: file slowly changes over time
    // run this from any MonoBehaviour with StartCoroutine
    public static IEnumerator CorruptStage(string fileName)
    {
        string[] stages = {
            "hello?",
            "hello? is anyone there",
            "the shelves are not empty",
            "you can't delete me from here"
        };

        foreach (string stage in stages)
        {
            WriteMessage(fileName, stage);
            yield return new WaitForSeconds(20f);
        }
    }
}
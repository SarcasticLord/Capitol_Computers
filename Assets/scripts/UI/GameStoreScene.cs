using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System.Diagnostics;

public class GameStoreScene : MonoBehaviour
{

    public GameObject blockSelected;
    public GameObject gooseSelected;
    public GameObject tweakSelected;

    public string blockScene = "blockblaster";
    public string rollScene = "RollaGoose";
    public string tweakScene = "Tweak.tv";


    private string selectedLevel = "";

    public void CapitolWindows() // OPENS WINDOWS OS
    {
        SceneManager.LoadScene("CapitolWindows");
    }

    public void StartBlock() // STARTS BLOCKBUSTER
    {
        SaveState.lastScene = "Blockblaster";
        SceneManager.LoadScene("BlockblasterMain");
        //WindowManager.instance.startBlockblaster.SetActive(true);
    }

    public void StartGoose() // START ROLL A MAZE
    {
        SaveState.lastScene = "RollaMaze";
        SceneManager.LoadScene("rollamaze");
    }

    

    public void SelectBlock()
    {
        blockSelected.SetActive(true);
        gooseSelected.SetActive(false);
        tweakSelected.SetActive(false);
        selectedLevel = blockScene;
        
    }

    public void SelectGoose()
    {
        blockSelected.SetActive(false);
        gooseSelected.SetActive(true);
        tweakSelected.SetActive(false);
        selectedLevel = rollScene;
    }

    public void SelectTweak()
    {
        blockSelected.SetActive(false);
        gooseSelected.SetActive(false);
        tweakSelected.SetActive(true);
        selectedLevel = tweakScene;
    }

    // public void LaunchTweakScene()
    // {
    //     WindowManager.pendingWindows = "Explorer";
    //     SceneManagerScript.instance.WindowsOS();
    // }

    public void Play()
    {
        if (string.IsNullOrEmpty(selectedLevel))
        {
            UnityEngine.Debug.Log("select a level");
            return;
        }

        if(selectedLevel == tweakScene)
        {
            LaunchProgram();
        }

        else
        {
            SceneManager.LoadScene(selectedLevel);
        }
    }
    // launches external programs
    public void LaunchProgram()
    {
        string appPath = "";

        #if UNITY_STANDALONE_WIN
            appPath = Path.Combine(
                Application.streamingAssetsPath,
                "TweakRemastered.exe"
            );
        #elif UNITY_STANDALONE_OSX
            appPath = Path.Combine(
                Application.streamingAssetsPath,
                "TweakRemastered.app"
            );
        #endif

        if (Directory.Exists(appPath) || File.Exists(appPath))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = appPath,
                UseShellExecute = true
            });
        }
        else
        {
            UnityEngine.Debug.LogError("Application not found: " + appPath);
        }
    }

}

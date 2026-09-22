using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStoreScene : MonoBehaviour
{

    public GameObject blockSelected;
    public GameObject gooseSelected;
    public GameObject startBlockblaster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        
    }

    public void SelectGoose()
    {
        blockSelected.SetActive(false);
        gooseSelected.SetActive(true);
    }
}

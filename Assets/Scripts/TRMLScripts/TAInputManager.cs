using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class TAInputManager : MonoBehaviour
{
    public static TAInputManager instance;

    public TMP_Text storyText; // the story 
    public TMP_InputField userInput; // the input field object
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText; // part of the input field for initial placeholder text'

    public ScrollRect scrollRect; // controls how our story scrolls
    
    private string story; // holds the story to display
    private List<string> commands = new List<string>();

        private void Awake()
        {
        // scene-bound on purpose: this object points at UI that lives in this scene,
        // so a fresh one has to take over every time the scene loads
        instance = this;

        story = storyText.text;

        // bring back the old terminal text if we're returning to this scene
        if (TAGameManager.instance != null && !string.IsNullOrEmpty(TAGameManager.instance.terminalLog))
        {
            story = TAGameManager.instance.terminalLog;
            storyText.text = story;
        }

        commands.Add("open");
        commands.Add("download");
        commands.Add("restart");
        commands.Add("save");
        commands.Add("inventory");
        commands.Add("commands");
        commands.Add("exit");

        userInput.onEndEdit.AddListener(GetInput);
        }

    void Start()
    {
        if (Statistics.instance != null)
        {
            Statistics.instance.stats.Terminal.TRMLattempts++;
            Statistics.instance.SaveStats();
        }

        StartCoroutine(ScrollToBottom());
    }

    private void OnDestroy()
    {
        if (userInput != null)
            userInput.onEndEdit.RemoveListener(GetInput);
 
        if (instance == this)
            instance = null;
    }

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    void GetInput(string input)
    {

        userInput.text = "";
        userInput.ActivateInputField();

        if (input != "")
        {
            char[] delims = { ' ' };

            string[] parts = input.ToLower().Split(delims); // parts[0] command, parts[1] direction or pickup

            if (parts.Length > 0)
            {
                if (commands.Contains(parts[0]))
                {
                    UpdateTerminal(input);
                    if (parts[0] == "open")
                    {
                        if (TANavigationManager.instance.SwitchFolders(parts[1]))
                            Debug.Log("That file exists");
                        else
                            UpdateTerminal("That file doesnt exist or permission denied");
                    }
                    else if (parts[0] == "download")
                    {
                        if (TANavigationManager.instance.getItem(parts[1]))
                            TAGameManager.instance.inventory.Add(parts[1]);
                    }
                    else if (parts[0] == "save") {
                        TAGameManager.instance.Save();
                        UpdateTerminal("Saving game...");
                    }

                    else if (parts[0] == "restart")
                        TANavigationManager.instance.GameRestart();
                    
                    else if (parts[0] == "exit")
                    {
                        TAGameManager.instance.Save();
                        
                        UpdateTerminal("Saving game...");
                        UpdateTerminal("Closing terminal...");
                        SceneManager.LoadScene("capitolWindows");

                    }
                        

                    else if (parts[0] == "inventory")
                    {
                        if(TAGameManager.instance.inventory.Count == 0)
                        {
                            UpdateTerminal("you didnt download any files.");
                        }
                        else
                        {
                            string items = "you downloaded: ";
                            foreach (string item in TAGameManager.instance.inventory)
                            {
                                items += item + " ";
                            }
                            UpdateTerminal(items);
                        }
                    }
                    else if (parts[0] == "commands")
                        UpdateTerminal("Commands: open, download, save, restart, inventory, exit");

                    else
                        UpdateTerminal("No file found with that name.");
                    
                }
                else
                {
                    UpdateTerminal("That file doesnt exist.");
                }
            }
        }


    }

    

    public void UpdateTerminal(string msg)
    {
        story += "\n" + msg;
        storyText.text = story;
 
        // stash it so it survives leaving the scene
        if (TAGameManager.instance != null)
            TAGameManager.instance.terminalLog = story;
 
        StartCoroutine(ScrollToBottom());
    }
}

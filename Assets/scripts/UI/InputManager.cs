using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public TMP_Text outputText; // the output 
    public TMP_InputField userInput; // the input field object
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText; // part of the input field for initial placeholder text

    public ScrollRect scrollRect; // controls how our cmd scrolls
    
    private string output; // holds the output to display
    private List<string> commands = new List<string>();

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
        commands.Add("0");
        commands.Add("1");
        commands.Add("2");
        commands.Add("3");
        commands.Add("8");
        commands.Add("9");
        
        output = outputText.text;
        userInput.onEndEdit.AddListener(GetInput);
    }

    
    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    void GetInput(string input) // player enters a number and it changes the scene
    {

        Updateoutput(input);
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
                    switch (parts[0])
                    {
                        case "0":
                            SceneManager.LoadScene("TitleScene");
                            Updateoutput("RESTARTING_BOOTLOADER");
                            break;

                        case "1":
                            SceneManager.LoadScene("OSloading");
                            Updateoutput("BOOTING_CAPITOL_COMPUTERS");
                            break;

                        case "2":
                            SceneManager.LoadScene("Credits");
                            Updateoutput("CAPITOL_COMPUTERS_CREDITS");
                            break;

                        case "3":
                            SceneManager.LoadScene("ExitScene");
                            Updateoutput("ERROR: EXIT_CAPITOL_COMPUTERS");
                            break;  

                        case "9":
                            SceneManager.LoadScene("GamesStore");
                            Updateoutput("STARTING_STORE_DEBUG");
                            break;

                        

                        

                        
                    }
                }
                else
                {
                    Updateoutput("ERROR: INVALID_OPTION_SELECTED");
                }
            }
        }


    }

    

    public void Updateoutput(string msg)
    {
        if (!Application.isPlaying) return;
        output += "\n" + msg;
        outputText.text = output;
        if (isActiveAndEnabled)
            StartCoroutine("ScrollToBottom");
        
    }
}

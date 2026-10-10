using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TAToggleController : MonoBehaviour
{

    public Image background;
    public TMP_Text storyText; // the story 
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText; // part of the input field for initial placeholder text'
    public Text toggleText;
    public Text musicText;

    private bool darkmode;
    private Toggle toggle;

    void Start()
    {
        //PlayerPrefs.HasKey("darkmode");
        darkmode = PlayerPrefs.GetInt("darkmode", 1) == 1? true : false; // 1 is the default = darkmode is true
        toggle = GetComponent<Toggle>();
        SetTheme();
        toggle.onValueChanged.AddListener(UpdateTheme);

        
    }
    void UpdateTheme(bool isChecked)
    {
        darkmode = isChecked;
        PlayerPrefs.SetInt("darkmode", darkmode ? 1 : 0);
        PlayerPrefs.Save();
        SetTheme();
    }

    void SetTheme()
    {
        if (darkmode)
        {
            toggle.isOn = true; // check the box
            background.color = Color.black;
            storyText.color = Color.white;
            inputText.color = Color.white;
            placeHolderText.color = Color.white;
            toggleText.color = Color.white;
            musicText.color = Color.white;
            TAInputManager.instance.UpdateTerminal("Turned on darkmode.");
        }
        else
        {   
            toggle.isOn = false; // uncheck the box     lightmode
            background.color = Color.white;
            storyText.color = Color.black;
            inputText.color = Color.white;
            placeHolderText.color = Color.white;
            toggleText.color = Color.black;
            musicText.color = Color.black;
            TAInputManager.instance.UpdateTerminal("MY EYES IT BURNSSSS!!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

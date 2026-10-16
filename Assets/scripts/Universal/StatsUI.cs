using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class StatsUI : MonoBehaviour
{
    [Header("RollaGoose Stats")]
    public List<TextMeshProUGUI> RollFastTime;
    public List<TextMeshProUGUI> RollRuns;

    [Header("BlockBlaster Stats")]
    public List<TextMeshProUGUI> BBFastTime;
    public List<TextMeshProUGUI> BBmostKilled;
    public List<TextMeshProUGUI> BBmostTrashColl;
    public List<TextMeshProUGUI> BBruns;

    [Header("Terminal Stats")]
    public List<TextMeshProUGUI> TRMLruns;

    [Header("UI Switching")]
    public GameObject AllStats;
    public GameObject BlockStats;
    public GameObject RollStats;
    public GameObject TRMLstats;


    //public TextMeshProUGUI FilePath;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // blockblaster
        SetText(BBFastTime, "Fastest Time: " + Statistics.instance.stats.BlockBlaster.BlockFastestTime.ToString("F2"));
        SetText(BBmostKilled, "Most Enemies Killed: " + Statistics.instance.stats.BlockBlaster.BlockMostEnemies.ToString());
        SetText(BBmostTrashColl, "Most Trash Collected: " + Statistics.instance.stats.BlockBlaster.BlockTrashCollected.ToString());
        SetText(BBruns, "Number of times Completed: " + Statistics.instance.stats.BlockBlaster.BlockTimesAttempted.ToString());


        // roll a goose
        SetText(RollRuns, "Number of times completed: " + Statistics.instance.stats.RollaGoose.RollAttempts.ToString());
        SetText(RollFastTime, "Fastest Time: " + Statistics.instance.stats.RollaGoose.RollFastestTime.ToString("F2"));

        // terminal
        SetText(TRMLruns, "Number of times opened: " + Statistics.instance.stats.Terminal.TRMLattempts.ToString());


        // extras
        //FilePath.text = "Statistics File Path:\n" + Application.persistentDataPath;


    }

    private void SetText(List<TextMeshProUGUI> targets, string value)
    {
        if (targets == null) return;

        foreach (TextMeshProUGUI t in targets)
        {
            if (t != null)
            {
                t.text = value;
            }
        }
    }


    public void ShowAllStats()
    {
        AllStats.SetActive(true);
        BlockStats.SetActive(false);
        RollStats.SetActive(false);
        TRMLstats.SetActive(false);
    }

    public void ShowBlockStats()
    {
        AllStats.SetActive(false);
        BlockStats.SetActive(true);
        RollStats.SetActive(false);
        TRMLstats.SetActive(false);    
    }

    public void ShowRollStats()
    {
        AllStats.SetActive(false);
        BlockStats.SetActive(false);
        RollStats.SetActive(true);
        TRMLstats.SetActive(false);    
    }

    public void ShowTRMLstats()
    {
        AllStats.SetActive(false);
        BlockStats.SetActive(false);
        RollStats.SetActive(false);
        TRMLstats.SetActive(true);    
    }

}


    
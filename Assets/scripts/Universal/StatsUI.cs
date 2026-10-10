using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{

    public TextMeshProUGUI RollFastTime;
    public TextMeshProUGUI RollRuns;

    public TextMeshProUGUI BBFastTime;
    public TextMeshProUGUI BBmostKilled;
    public TextMeshProUGUI BBmostTrashColl;
    public TextMeshProUGUI BBruns;

    public TextMeshProUGUI TRMLruns;

    //public TextMeshProUGUI FilePath;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // blockblaster
        BBFastTime.text = "Fastest Time: " + Statistics.instance.stats.BlockBlaster.BlockFastestTime.ToString("F2");
        BBmostKilled.text = "Most Enemies Killed: " + Statistics.instance.stats.BlockBlaster.BlockMostEnemies.ToString();
        BBmostTrashColl.text = "Most Trash Collected: " + Statistics.instance.stats.BlockBlaster.BlockTrashCollected.ToString();
        BBruns.text = "Number of times Completed: " + Statistics.instance.stats.BlockBlaster.BlockTimesAttempted.ToString();


        // roll a goose
        RollRuns.text = "Number of times completed: " + Statistics.instance.stats.RollaGoose.RollAttempts.ToString();
        RollFastTime.text = "Fastest Time: " + Statistics.instance.stats.RollaGoose.RollFastestTime.ToString("F2");

        // terminal
        TRMLruns.text = "Number of times opened: " + Statistics.instance.stats.Terminal.TRMLattempts.ToString();


        // extras
        //FilePath.text = "Statistics File Path:\n" + Application.persistentDataPath;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

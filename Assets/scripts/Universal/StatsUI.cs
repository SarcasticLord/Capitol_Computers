using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{

    public TextMeshProUGUI RollFastTime;
    public TextMeshProUGUI BBFastTime;
    public TextMeshProUGUI BBmostKilled;
    public TextMeshProUGUI BBmostTrashColl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RollFastTime.text = "Fastest Time: " + Statistics.instance.stats.RollaGoose.RollFastestTime.ToString("F2");

        BBFastTime.text = "Fastest Time: " + Statistics.instance.stats.BlockBlaster.BBfastestTime.ToString("F2");
        BBmostKilled.text = "Most Enemies Killed: " + Statistics.instance.stats.BlockBlaster.BBmostEnemies.ToString();
        BBmostTrashColl.text = "Most Trash Collected: " + Statistics.instance.stats.BlockBlaster.BBmostTrash.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

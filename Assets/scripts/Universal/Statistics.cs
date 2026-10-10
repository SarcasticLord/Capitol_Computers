using UnityEngine;
using System.IO;

[System.Serializable]
public class BlockStats
{
    // BlockBlaster
    public float BlockFastestTime = 0;
    public int BlockMostEnemies = 0;
    public int BlockTrashCollected = 0;
    public int BlockTimesAttempted = 0;

}

[System.Serializable]
public class RollStats
{
    // RollaGoose
    public float RollFastestTime = 0;
    public int RollAttempts = 0;
}

[System.Serializable]
public class TRMLstats
{
    // Terminal
    public int TRMLattempts = 0;

}

[System.Serializable]

public class StatisticsData
{
    public BlockStats BlockBlaster = new BlockStats();
    public RollStats RollaGoose = new RollStats();
    public TRMLstats Terminal = new TRMLstats();


}

public class Statistics : MonoBehaviour
{

    
    public static Statistics instance = null;
    public StatisticsData stats = new StatisticsData();

    string savePath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("dont destroy statistics");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        savePath = Path.Combine(FilePaths.GetCapitolFolder()) + "/Statistics.json";

        LoadStats();
    }

    public void SaveStats()
    {
        string json = JsonUtility.ToJson(stats, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Loaded Stats");
    }

    public void LoadStats()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            stats = JsonUtility.FromJson<StatisticsData>(json);
            Debug.Log("Loaded Stats");
        } 
        else 
        {
            Debug.Log("no stats");

            stats = new StatisticsData();
            SaveStats();
        }
    }


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

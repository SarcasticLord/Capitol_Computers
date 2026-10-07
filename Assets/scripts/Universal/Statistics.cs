using UnityEngine;
using System.IO;

[System.Serializable]
public class BBstats
{
    // BlockBlaster
    public float BBfastestTime = 0;
    public int BBmostEnemies = 0;
    public int BBmostTrash = 0;
}

[System.Serializable]
public class RollStats
{
    // RollaGoose
    public float RollFastestTime = 0;
}

[System.Serializable]
public class TAstats
{
    // Terminal
    public float TAruns = 0;

}

[System.Serializable]

public class StatisticsData
{
    public BBstats BlockBlaster = new BBstats();
    public RollStats RollaGoose = new RollStats();
    public TAstats Terminal = new TAstats();


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
        
        savePath = Application.persistentDataPath + "/Statistics.json";

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

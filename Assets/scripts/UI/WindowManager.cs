using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    [System.Serializable]

    public struct WindowEntry 
    {
        public string id;
        public Window window;
    }

    public List<WindowEntry> windowList;
    private Dictionary<string, Window> windows;

    void Awake() 
    {
        if (instance == null) instance = this;
        else if (instance != this) { Destroy(gameObject); return; }

        windows = new Dictionary<string, Window>();
        foreach (var entry in windowList)
            windows[entry.id] = entry.window;
    }

    public void OpenWindow(string id)
    {
        if (windows.TryGetValue(id, out var win)) win.Open();
    }

    public void CloseWindow(string id)
    {
        if (windows.TryGetValue(id, out var win)) win.Close();
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

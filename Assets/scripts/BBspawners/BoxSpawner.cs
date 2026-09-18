using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour
{

    public Transform BoxPoint;
    public GameObject Box; 
    public GameObject BoxExists;

    public bool facingRight = true;

    void Start()
    {
        StartCoroutine(BoxSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BoxSpawn() 
    {
        while (true)
        {
            if (BoxExists == null)
            {
                BoxExists = Instantiate(Box, BoxPoint.position, facingRight ? BoxPoint.rotation : Quaternion.Euler(-90, 0, 0));

            
            }
            yield return new WaitForSeconds(60f);  
        } 
    }
}

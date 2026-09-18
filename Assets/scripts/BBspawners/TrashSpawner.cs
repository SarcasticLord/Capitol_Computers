using UnityEngine;
using System.Collections;


public class TrashSpawner : MonoBehaviour
{
    public Transform TrashPoint;
    public GameObject Trash; 
    public GameObject TrashExists;

    public bool facingRight = true;

    
    void Start()
    {
        StartCoroutine(TrashSpawn());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TrashSpawn() 
    {
        while (true)
        {
            if (TrashExists == null)
            {
                TrashExists = Instantiate(Trash, TrashPoint.position, facingRight ? TrashPoint.rotation : Quaternion.Euler(-90, 0, 0));

                
            }
            yield return new WaitForSeconds(300F);  // 5 minutes
        } 
    }
    
}

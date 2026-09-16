// this script goes on the boxes
// when the boxes hit the enemy it does damage

// now you may be asking why not put it on the enemy?    good question

using UnityEngine;

public class ThrowDamage : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject); // the item
        }
          
    }
}

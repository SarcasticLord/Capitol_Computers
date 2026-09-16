 // this script goes on the shelf object
 
 using UnityEngine;

public class ShelfSwapping : MonoBehaviour
{

    public GameObject[] shelves;
    public int shelfIndex;
    [SerializeField] PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickUp")) // reminder that pickup is the box i should change the name
        {
            if (shelfIndex >= shelves.Length - 1) return;

            GameObject box = other.gameObject;
            
            if (shelfIndex < shelves.Length - 1)
            {
                int newShelfIndex = shelfIndex + 1;
                GameObject newShelf = Instantiate(shelves[newShelfIndex], transform.position, transform.rotation); // little asset swapping it takes the index of shelf objects and swaps to the next one in the list
                newShelf.GetComponent<ShelfSwapping>().shelfIndex = newShelfIndex;
                Destroy(gameObject);

                if (newShelfIndex == 5) // when the full shelf is done it adds a point to the shelf counter 
                {
                    GameManager.instance.stockCount++; 
                    GameManager.instance.StockObjective();
                }

                
                
            }
            Destroy(box);
        }
    }
}

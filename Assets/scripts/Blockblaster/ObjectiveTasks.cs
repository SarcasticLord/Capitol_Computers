// this is the objective controller where all the raycasts live
// this was 2 scripts but now its 1 becuase they are basically the same 

// so this is how i set it up for the player to hit E and pick up the box
// this is a change from the old way where you can just run into the box and itll snap to the point

// this is also the thing that cleans up trash

using UnityEngine;
//using system.Collections;

public class ObjectiveTasks : MonoBehaviour
{
    public Camera camera;
    public PlayerController player;
    public Transform BoxSnap;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // this is here so i can see the ray without needing to hit E or MB0
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);
        
        if (Input.GetMouseButtonDown(0)) 
        {
            CleanTrash();
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupBox();
        }
    }

    void CleanTrash() // checking for broom and raycasting on left click
    {
        if (player.HoldingBroom() == false) return;

        Ray ray = camera.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);

        if (Physics.Raycast(ray, out hit, 5f))
        {
            Debug.Log("HIT: " + hit.transform.name);

            ObjectiveTasks trash = hit.transform.GetComponentInParent<ObjectiveTasks>();

            if (hit.transform.CompareTag("trash"))
            {
                GameManager.instance.trashCount++;  // when the raycast hits it updates the trash counter
                GameManager.instance.TrashObjective();

                Destroy(hit.collider.gameObject);
            }

        }

    }

    void PickupBox() // checking if your holding an item and picks up the box
    {
        if (player.holdItem != null) return;

        Ray ray = camera.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);

        if (Physics.Raycast(ray, out hit, 5f))
        {
            Debug.Log("HIT: " + hit.transform.name);


            if (hit.transform.CompareTag("pickUp")) // snapping the box to the snap point
            {
                //holdItem = gameObject;

                hit.transform.gameObject.transform.position = BoxSnap.position;
                hit.transform.gameObject.transform.rotation = BoxSnap.rotation;
                hit.transform.gameObject.transform.SetParent(BoxSnap);

                hit.transform.GetComponent<BoxMovement>().StopRotating(); // stops the box spinning
            }

        }

    }
}

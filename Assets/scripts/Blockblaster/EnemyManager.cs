using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public float health = 100;
    public float movementSpeed = 5f;
    public GameObject player;
    private NavMeshAgent enemy;
    public PlayerHealth playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player"); // work arounds because the inspector doesent work with this script ???

        if (playerHealth == null && player != null)
            playerHealth = player.GetComponent<PlayerHealth>();

        enemy.speed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider collision) // enemy damage when the items hit it 
    {
        if (collision.gameObject.CompareTag("throwable")) // objects hitting enemies
            health += -25;

        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.netflixCount++;  // after killing an employee get a point      you need at least 20-50 to win
            GameManager.instance.MetflicksObjective();
        }

        if (collision.CompareTag("Player")) // does player damage
        {
            playerHealth.TakeDamage(5);
        }
            
        
    }
}

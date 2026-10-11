using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public GameObject loseTextObject;
    public PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
        loseTextObject.SetActive(false);
    }
    public Slider slider;

    public void TakeDamage(int amount) // takes damage right    this also adjsuts the cool slider
    {
        health -= amount;
        slider.value = health;
        if (health <= 0)
        {
            
            loseTextObject.SetActive(true);
            PlayerController.instance.Invoke("Die", 3f);
        }
        
    }
    

}



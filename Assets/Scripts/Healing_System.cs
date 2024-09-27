using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing_System : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        
        // Ellenőrizzük, hogy a játékos életpontja elérte-e a maximális szintet
        if (playerHealth != null && playerHealth.currentHealth < playerHealth.maxHealth)
        {
            // Ha nincs tele az élete, akkor gyógyítunk
            playerHealth.ChangeHealth(1);

            // Gyógyító tárgyat inaktívvá tesszük
            gameObject.SetActive(false);
        }

    }
    
}
/*
        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(1);
    
        gameObject.SetActive(false);
*/
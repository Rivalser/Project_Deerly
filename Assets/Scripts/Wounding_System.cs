using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wounding_System : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        
        // Ellenőrizzük, hogy a játékos nem halott-e
        if (playerHealth != null && playerHealth.currentHealth <= playerHealth.maxHealth)
        {
            // Ha nem halott sebződik
            playerHealth.ChangeHealth(-1);

            // Sebző tárgyat inaktívvá tesszük
            gameObject.SetActive(false);
            // Megoldani, ha ez öli meg a játékost, akkor végbe kell mennie a halál animációnak és a pályának ujra kell indulnia. Eredetiről
        }

    }
    
}

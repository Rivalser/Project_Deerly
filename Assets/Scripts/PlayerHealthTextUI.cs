using UnityEngine;
using TMPro;

public class PlayerHealthTextUI : MonoBehaviour
{
    public TMP_Text healthText;
    private PlayerHealth playerHealth;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = $"HP: {playerHealth.currentHealth} / {playerHealth.maxHealth}";
        }
    }
}

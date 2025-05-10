using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    public GameObject gameOverPanel;

    private bool isDead = false;

    private void Start()
    {
        UpdateHealthUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ChangeHealth(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            isDead = true;
            Time.timeScale = 0f;
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }
    }

    public void SetHealth(float amount)
    {
        currentHealth = Mathf.Clamp(Mathf.RoundToInt(amount), 0, maxHealth);
        UpdateHealthUI();
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public float GetHealth() => currentHealth;
    public Vector3 GetPosition() => transform.position;

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "HP: " + currentHealth + " / " + maxHealth;

        if (healthTextAnim != null)
            healthTextAnim.Play("TextUpdate");
    }
}
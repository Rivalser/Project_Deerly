using UnityEngine;
using System;

public class Enemy_Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public string uniqueID;
    public bool isAlive = true;

    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueID))
            uniqueID = Guid.NewGuid().ToString();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    public float GetHealth() => currentHealth;
    public Vector3 GetPosition() => transform.position;

    public void SetHealth(float amount)
    {
        currentHealth = Mathf.Clamp(Mathf.RoundToInt(amount), 0, maxHealth);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}

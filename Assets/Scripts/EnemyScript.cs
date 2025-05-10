using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public string uniqueID;
    public int health = 5;
    public bool isAlive = true;

    private void Awake()
    {
        if (string.IsNullOrEmpty(uniqueID))
            uniqueID = Guid.NewGuid().ToString();
    }

    public void Die()
    {
        isAlive = false;
        gameObject.SetActive(false);
    }
}

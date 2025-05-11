using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Enemy_Health[] enemies;
    private PickUpItem[] items;

    [Header("Nyilak, amiket aktiválni kell")]
    public GameObject[] arrowsToActivate;

    private bool arrowsActivated = false;
    private float checkDelay = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(CheckConditions), checkDelay, checkDelay);
    }

    private void CheckConditions()
    {
        if (arrowsActivated) return;

        enemies = FindObjectsOfType<Enemy_Health>();
        items = FindObjectsOfType<PickUpItem>();

        bool allEnemiesDead = true;
        bool allItemsCollected = true;

        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.isAlive)
            {
                allEnemiesDead = false;
                break;
            }
        }

        foreach (var item in items)
        {
            if (item != null && !item.isCollected)
            {
                allItemsCollected = false;
                break;
            }
        }

        if (allEnemiesDead && allItemsCollected)
        {
            arrowsActivated = true;
            Debug.Log("Minden enemy és tárgy teljesítve — nyilak aktiválva!");

            foreach (var arrow in arrowsToActivate)
            {
                if (arrow != null)
                    arrow.SetActive(true);
            }
        }
    }
}

using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public bool isCollected = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.H) && !isCollected)
        {
            isCollected = true;
            Debug.Log($"{gameObject.name} felvéve!");
            gameObject.SetActive(false);
        }
    }
}

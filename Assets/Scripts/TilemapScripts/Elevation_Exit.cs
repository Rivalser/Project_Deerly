using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap kezeléséhez kell!

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;
    public GameObject treesParent; // A "Trees" objektum

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;
            }

            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = false;
            }

            // Játékos sorting order visszaállítása 5-re
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;

            // Összes Tilemap sorting order visszaállítása a "Trees" objektumban
            if (treesParent != null)
            {
                Tilemap[] tilemaps = treesParent.GetComponentsInChildren<Tilemap>(); // Lekér minden Tilemap-et a Trees-ben

                foreach (Tilemap tilemap in tilemaps)
                {
                    tilemap.GetComponent<TilemapRenderer>().sortingOrder = 5;
                }
            }
            else
            {
                Debug.LogWarning("Trees objektum nincs beállítva az Inspectorban!");
            }
        }
    }
}

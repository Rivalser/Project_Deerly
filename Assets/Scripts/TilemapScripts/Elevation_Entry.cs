using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap kezeléséhez kell!

public class Elevation_Entry : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;
    public GameObject treesParent; // A Trees objektum, amit Inspectorban kell beállítani

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;
            }

            foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = true;
            }

            // Játékos sorting order módosítása
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;

            // Összes Tilemap sorting order módosítása a "Trees" objektumban
            if (treesParent != null)
            {
                Tilemap[] tilemaps = treesParent.GetComponentsInChildren<Tilemap>(); // Lekér minden Tilemap-et a Trees-ben

                foreach (Tilemap tilemap in tilemaps)
                {
                    tilemap.GetComponent<TilemapRenderer>().sortingOrder = 15;
                }
            }
            else
            {
                Debug.LogWarning("Trees objektum nincs beállítva az Inspectorban!");
            }
        }
    }
}

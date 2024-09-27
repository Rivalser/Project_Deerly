using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation_Entry : MonoBehaviour
{

    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player")
        {
           
            foreach(Collider2D mountain in mountainColliders)
            {
                //Debug.Log("Ütközés történt"); 
                mountain.enabled = false;
            }

            foreach(Collider2D boundary in boundaryColliders)
            {
                //Debug.Log("Ütközés történt"); 
                boundary.enabled = true;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }

    }

}

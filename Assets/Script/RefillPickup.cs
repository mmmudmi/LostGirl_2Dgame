using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillPickup : MonoBehaviour
{
    [SerializeField] AudioClip refillPickupSFX;
    //[SerializeField] int refillValue = 2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Calling the public method on the GameSession object
            //FindObjectOfType<GameSession>().AddToAmmo(refillValue);
            FindObjectOfType<GameSession>().AddToAmmo(2);


            // play at the camera
            AudioSource.PlayClipAtPoint(refillPickupSFX, Camera.main.transform.position,.3f); 
            Destroy(gameObject);
        }
    }
}

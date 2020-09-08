using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   
    public GameObject Explosionmm;
   // public GameObject audioexplo;
    public GameObject flames;

    
    void FixedUpdate()
    {
      


    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Destroy(collision.gameObject);
            Instantiate(Explosionmm, gameObject.transform.position, Quaternion.identity);
           // Instantiate(audioexplo, gameObject.transform.position, Quaternion.identity);
            Instantiate(flames, gameObject.transform.position, Quaternion.identity);

            print("si");
            Destroy(gameObject);
            Destroy(collision.gameObject);
            //explotiontank.activeexplotion(true);
            // Debug.Log(collision);

        }
    }
}

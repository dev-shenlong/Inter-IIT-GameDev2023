using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemcollectable : MonoBehaviour
{
    private int collectItem=0;  //Collected items here(Give point say 10)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("collect"))
        {
            Destroy(collision.gameObject);
            collectItem += 30;
            Debug.Log("score: " + collectItem);
        }


       else if (collision.gameObject.CompareTag("collect2"))
        {
            Destroy(collision.gameObject);
            collectItem += 60;
            Debug.Log("score: " + collectItem);
        }

        else if (collision.gameObject.CompareTag("collect3"))
        {
            Destroy(collision.gameObject);
            collectItem +=100;
            Debug.Log("score: " + collectItem);
        }


    }
}

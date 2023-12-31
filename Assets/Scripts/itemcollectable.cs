using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemcollectable : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioSource CollectSoundEffect;
    public int collectItem=0;  //Collected items here(Give point say 10)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("collect"))
        {
            CollectSoundEffect.Play();
            Destroy(collision.gameObject);
            collectItem += 30;
            Debug.Log("score: " + collectItem);
            scoreText.text = "SCORE: " + collectItem.ToString();
        }


       else if (collision.gameObject.CompareTag("collect2"))
        {
            CollectSoundEffect.Play();
            Destroy(collision.gameObject);
            collectItem += 60;
            Debug.Log("score: " + collectItem);
            scoreText.text = "SCORE: " + collectItem.ToString();
        }

        else if (collision.gameObject.CompareTag("collect3"))
        {
            CollectSoundEffect.Play();
            Destroy(collision.gameObject);
            collectItem +=100;
            Debug.Log("score: " + collectItem);
            scoreText.text = "SCORE: " + collectItem.ToString();
        }


    }
}

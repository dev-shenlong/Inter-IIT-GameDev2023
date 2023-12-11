using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startpos;
    SpriteRenderer sprite;
    Rigidbody2D rb;



    private void Start()
    {
        startpos = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Traps"))
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(1f));
    }

   IEnumerator Respawn(float duration)
    {
        rb.simulated = false;
        rb.velocity = new Vector2(0, 0);
        sprite.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startpos;
        sprite.enabled = true;
        rb.simulated = true;
    }




}

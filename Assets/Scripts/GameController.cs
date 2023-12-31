using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Health PlayerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision object is not null and has the "Traps" tag
        if (collision != null && collision.CompareTag("Traps"))
        {
            StartCoroutine(ApplyDamageWithDelay());
        }
    }

    private IEnumerator ApplyDamageWithDelay()
    {
        PlayerHealth.TakeDamage(damage);
        yield return new WaitForSeconds(3f);
        // Additional code to execute after the delay
    }
}

    //void Die()
    //{
        
    //}

   //IEnumerator Respawn(float duration)
   // {
   //     rb.simulated = false;
   //     rb.velocity = new Vector2(0, 0);
   //     sprite.enabled = false;
   //     yield return new WaitForSeconds(duration);
   //     transform.position = startpos;
   //     sprite.enabled = true;
   //     rb.simulated = true;
   // }






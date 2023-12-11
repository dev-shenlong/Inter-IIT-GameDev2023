using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    private Animator anim;
    private Rigidbody2D rb;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("trap"))
        {
            TakeDamage(20);
            Die();

        }
    }
    

    private void Die()
    {
        Debug.Log(currentHealth);
        if (currentHealth == 0) 
        {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

        }
    }

    private void RestartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
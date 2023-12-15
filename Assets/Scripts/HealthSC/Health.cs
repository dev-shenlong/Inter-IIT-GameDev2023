using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Health : MonoBehaviour
{
    [SerializeField] private float StartingHealth;
    [SerializeField] private GameObject gameOverMenu;
    public float currentHealth { get; private set; }
    private bool dead;
    


    private void Awake()
    {
        currentHealth = StartingHealth;
        
    }

    public void TakeDamage(float _damage) 
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, StartingHealth);


        if(currentHealth>0)
        {
            //Player is hurt

        }
        else

        {
            if(!dead)
            {
                //GetComponent<Playermovement>().enabled = false;
                //Player is dead
                dead = true;

                Time.timeScale = 0f;
                gameOverMenu.SetActive(true);
            }
        }


    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, StartingHealth);

    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.E))
    //    {
    //        TakeDamage(1);
    //    }
    //}



}

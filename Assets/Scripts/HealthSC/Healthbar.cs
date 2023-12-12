using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{

    [SerializeField] private Health PlayerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;
    




    // Start is called before the first frame update
    void Start()
    {
        totalHealthBar.fillAmount = PlayerHealth.currentHealth / 10;

    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = PlayerHealth.currentHealth / 10;
        
    }

    
}

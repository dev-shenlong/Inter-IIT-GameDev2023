using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            // Find all objects with the "Break1" tag and destroy them
            GameObject[] breakObjects = GameObject.FindGameObjectsWithTag("Break3");

            foreach (GameObject breakObject in breakObjects)
            {
                Destroy(breakObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{

    private float TimTilSpawn;
    public float StartTimeTillSpawn;
    public GameObject Lazer;
    public Transform WhereToTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   private  void Update()
    {
        if(TimTilSpawn<=0)
        {
            Instantiate(Lazer, WhereToTransform.position, WhereToTransform.rotation);
            TimTilSpawn = StartTimeTillSpawn;
        }

        else
        {
            TimTilSpawn -= Time.deltaTime;
        }
    }
}

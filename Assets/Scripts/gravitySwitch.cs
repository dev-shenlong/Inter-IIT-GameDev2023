using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitySwitch : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.y);
        if((transform.position.y>=0 && transform.position.y<=5) || (transform.position.y>=10 && transform.position.y<=15) || (transform.position.y>=20 && transform.position.y<=25) || (transform.position.y>=30 && transform.position.y<=35)){
            rb.gravityScale=1;
            transform.eulerAngles = new Vector3(0,0,0);
        }else{
            rb.gravityScale=-1;
            transform.eulerAngles = new Vector3(0,0,180f);
        }
    }
}

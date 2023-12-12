using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravitySwitch : MonoBehaviour
{
    private Rigidbody2D rb;
    public movement script;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position.y>=0 && transform.position.y<=5) || (transform.position.y>=10 && transform.position.y<=15)){
            rb.gravityScale=-1;
        }else{
            rb.gravityScale=1;
        }
    }
}

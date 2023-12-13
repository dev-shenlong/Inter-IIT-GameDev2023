using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    [SerializeField] private LayerMask groundLayer;
    //[SerializeField] private LayerMask wallLayer;
    private BoxCollider2D boxCollider;
    private float horizontalInput;


    // Start is called before the first frame update
    private void Awake()
    {
        
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //if (horizontalInput < 0.01f)
        //    transform.localScale = Vector3.one;
        //else if (horizontalInput > -0.01f)
        //    transform.localScale = new Vector3(-1, 1, 1);
        //Debug.Log(isGrounded());

    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    //private bool onWall()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    //    return raycastHit.collider != null;
    //}
    public bool canAttack()
    {
        return horizontalInput == 0;
    }
}



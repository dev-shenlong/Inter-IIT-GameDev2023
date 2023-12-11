using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortallerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform desination;
    GameObject player;
    Animator anim;
    Rigidbody2D playerrb;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        playerrb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            if(Vector2.Distance(player.transform.position,transform.position) > 0.3f)
            {

                StartCoroutine(PortalIn());

            }

        }
    }



    IEnumerator PortalIn()
    {
        playerrb.simulated = false;
        anim.Play("Player_teleport");
        StartCoroutine(MoveinPortal());
        yield return new WaitForSeconds(0.5f);
        player.transform.position = desination.transform.position;
        playerrb.velocity = Vector2.zero; 
        anim.Play("Player_outt_teleport");
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Moved");
        playerrb.simulated = true;
    }


    IEnumerator MoveinPortal()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}

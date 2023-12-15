using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform Destination;
    GameObject player;
    Animation anim;
    Rigidbody2D playerrb;
    [SerializeField] private AudioSource TeleportSoundEffect;

    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerrb = player.GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Vector2.Distance(player.transform.position,transform.position)>1f)
            {
                StartCoroutine(PortalIn());

            }
        }

    }


    IEnumerator PortalIn()
    {
        playerrb.simulated = false;
        anim.Play("portal IN");
       // TeleportSoundEffect.Play();
        StartCoroutine(MoveinPortal());
        yield return new WaitForSeconds(1f);

        TeleportSoundEffect.Play();
        player.transform.position = Destination.transform.position;
        playerrb.velocity = Vector2.zero;

        anim.Play("portal out");
        yield return new WaitForSeconds(1f);
        
        playerrb.simulated = true;
    }



    IEnumerator MoveinPortal()
    {
        float timer = 0;
        while (timer < 1f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }


}

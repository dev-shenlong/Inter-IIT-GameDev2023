using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchButton : MonoBehaviour
{
    [SerializeField] private GameObject[] Portals;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any necessary update logic here
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        anim.SetBool("goDown", true);

        // Loop through each portal in the array and set them active
        foreach (GameObject portal in Portals)
        {
            portal.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("goDown", false);

        // Loop through each portal in the array and set them inactive
        foreach (GameObject portal in Portals)
        {
            portal.SetActive(false);
        }
    }
}

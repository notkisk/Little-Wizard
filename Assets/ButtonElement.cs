using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonElement : MonoBehaviour
{

    public GameObject theThing;
    public GameObject door;
    bool isPushed=false;
  

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (!isPushed)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("Block"))
            {
                if (theThing)
                    theThing.SetActive(false);
                isPushed=true;
                door.SetActive(false);
            }
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPushed)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("Block"))
            {
                if (theThing)
                    theThing.SetActive(true);
                isPushed = false;
                door.SetActive(true);

            }
        }
       
    }
}

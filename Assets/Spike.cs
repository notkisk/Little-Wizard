using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject deathFX;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(deathFX, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1.5f, 1f));
            FindObjectOfType<AudioManager>().Play("Death");
        }
    }
}

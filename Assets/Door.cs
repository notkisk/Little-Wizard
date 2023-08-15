using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject deathFX;

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.TryGetComponent<EnemyController>(out EnemyController enemyController);
            if (enemyController.canMove)
            {
                enemyController.Freeze();
            }

        }  if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(deathFX, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1.5f, 1f));
            FindObjectOfType<AudioManager>().Play("Death");
        }
    }
}

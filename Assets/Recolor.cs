using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Recolor : MonoBehaviour
{
    public bool isBlue,isRed;
    public Color color;
    public GameObject deathFX;
    // Start is called before the first frame update
    private void Start()
    {
        transform.DORotate(new Vector3(0f,0f,190f),1f).SetLoops(-1,LoopType.Incremental);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            transform.DOPunchScale(new Vector3(0.25f, 0.25f, 0f),0.7f,3,0.1f);
            collision.TryGetComponent<Snowball>(out Snowball snowBall);
            snowBall.circleRenderer.color = color;
            if (isBlue)
            {
                snowBall.isRed = false;
                snowBall.isGreen = false;
                snowBall.isBlue = true;
            }
            else if (isRed)
            {
                snowBall.isRed = true;
                snowBall.isGreen = false;
                snowBall.isBlue = false;
            }
        }
        if (collision.gameObject.CompareTag("Player")&&collision.gameObject.GetComponent<ShotHandler>().isTraveling==false)
        {
            if (isBlue)
            {
                if (collision.TryGetComponent<ShotHandler>(out ShotHandler shotHandler))
                {
                    if (shotHandler.snowBallTemp)
                    {
                        if (shotHandler.snowBallTemp.isBlue)
                        {
                            Instantiate(deathFX, collision.transform.position, Quaternion.identity);
                            Destroy(collision.gameObject);
                            StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1.5f, 1f));
                            FindObjectOfType<AudioManager>().Play("Death");
                        }

                    }
                    else
                    {
                        Instantiate(deathFX, collision.transform.position, Quaternion.identity);
                        Destroy(collision.gameObject);
                        StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1.5f, 1f));
                        FindObjectOfType<AudioManager>().Play("Death");

                    }

                }
             
            }
            else if (isRed)
            {
                if (collision.TryGetComponent<ShotHandler>(out ShotHandler shotHandler))
                {
                    if (shotHandler.snowBallTemp)
                    {
                        if (shotHandler.snowBallTemp.isRed)
                        {
                            Instantiate(deathFX, collision.transform.position, Quaternion.identity);
                            Destroy(collision.gameObject);
                            StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1.5f, 1f));
                            FindObjectOfType<AudioManager>().Play("Death");
                        }

                    }
                    else
                    {
                        Instantiate(deathFX, collision.transform.position, Quaternion.identity);
                        Destroy(collision.gameObject);
                        StartCoroutine(FindObjectOfType<SceneController>().ReloadScene(1.5f, 1f));
                        FindObjectOfType<AudioManager>().Play("Death");
                    }

                }
             
            }
           
           
        }
    }
}

using DG.Tweening;
using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    bool isEnabled;

    int nextSceneLoad;

    bool hasFinished=false;

    public GameObject deathFX;
    // Import the javascript function that redirects to another URL
    [
    DllImport("__Internal")
    ]
    private
    static
    extern
    void
    RedirectTo
    ();
    // Import the javascript function that redirects to another URL
    [
    DllImport("__Internal")
    ]
    private
    static
    extern
    void
    StartGameEvent
    ();
    // Import the javascript function that redirects to another URL
    [
    DllImport("__Internal")
    ]
    private
    static
    extern
    void
    StartLevelEvent
    (int level);
    // Import the javascript function that redirects to another URL
    [
    DllImport("__Internal")
    ]
    private
    static
    extern
    void
    ReplayEvent
    ();



    private void Awake()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 20)
            {
                hasFinished = true;
                CameraShaker.Instance.ShakeOnce(4f, 6f, 0.05f, 0.05f);
                transform.DOMove(collision.transform.position, 0.65f).SetEase(Ease.OutQuart);
                GetComponentInChildren<PlayerController>().enabled = false;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponentInChildren<Animator>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine(FindObjectOfType<SceneController>().LoadScene(2f, 0));
                FindObjectOfType<AudioManager>().Play("Win");
            }
            else
            {
                hasFinished = true;
                CameraShaker.Instance.ShakeOnce(4f, 6f, 0.05f, 0.05f);
                transform.DOMove(collision.transform.position, 0.65f).SetEase(Ease.OutQuart);
                GetComponentInChildren<PlayerController>().enabled = false;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponentInChildren<Animator>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine(FindObjectOfType<SceneController>().LoadNextScene(1f, 0.5f));
                FindObjectOfType<AudioManager>().Play("Win");
                if (nextSceneLoad>PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                }
            }
        }

      
    }
    private void Update()
    {
       
        if (hasFinished)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }
}

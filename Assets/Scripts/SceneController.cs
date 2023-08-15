using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Runtime.InteropServices;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject _sprite;
    Vector3 startingSize;

    //// Import the javascript function that redirects to another URL
    //[
    //DllImport("__Internal")
    //]
    //private
    //static
    //extern
    //void
    //RedirectTo
    //();
    //// Import the javascript function that redirects to another URL
    //[
    //DllImport("__Internal")
    //]
    //private
    //static
    //extern
    //void
    //StartGameEvent
    //();
    //// Import the javascript function that redirects to another URL
    //[
    //DllImport("__Internal")
    //]
    //private
    //static
    //extern
    //void
    //StartLevelEvent
    //(int level);
    //// Import the javascript function that redirects to another URL
    //[
    //DllImport("__Internal")
    //]
    //private
    //static
    //extern
    //void
    //ReplayEvent
    //();
    private void Awake()
    {
        _sprite.SetActive(true);
        startingSize = _sprite.transform.localScale;
        instance = this;
        
    }

    private void Start()
    {
        _sprite.transform.DOScale(Vector3.zero,1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadScene(0f, 1f));

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex!=0)
            {
                StartCoroutine(LoadScene(1f, 0));
            }
        }

    }
    public IEnumerator ReloadScene(float InAnimationDelay,float delay)
    {
        Debug.Log("reload Scene");
//#if UNITY_EDITOR==false
//        //ReplayEvent();

//#endif
        yield return new WaitForSeconds(InAnimationDelay);
        ScaleUp();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator LoadNextScene(float InAnimationDelay, float delay)
    {
        Debug.Log("LoadNextScene");
        yield return new WaitForSeconds(InAnimationDelay);
        ScaleUp();
        yield return new WaitForSeconds(delay);
//#if UNITY_EDITOR==false
//        //StartLevelEvent(SceneManager.GetActiveScene().buildIndex);

//#endif
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public IEnumerator LoadScene(float delay,int index)
    {
        ScaleUp();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);

    }

    void ScaleUp()
    {
        _sprite.transform.DOScale(startingSize, 2f);

    }
    public void ReloadSceneTester(float inAnimation,float delay)
    {
        StartCoroutine(ReloadScene(inAnimation,delay));
    }

    public void LoadSceneUI(int index) { StartCoroutine(LoadScene(1f, index)); }
}

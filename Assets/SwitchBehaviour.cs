using DG.Tweening;
using EZCameraShake;
using System.Diagnostics;
using UnityEngine;

enum State
{
    Active,Disabled
}
public class SwitchBehaviour : MonoBehaviour
{
    public bool isMainCharacter;
    public GameObject outline;
    public GameObject mainBody;
    [HideInInspector]
     State state;
    public GameObject switchVFX;

    private void Awake()
    {
     

        if (isMainCharacter==false)
        {

            this.enabled = false;
        }
        else
        {

        }

        CheckState();

    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.GetComponent<CapsuleCollider2D>());
            CameraShaker.Instance.ShakeOnce(3f,7f,0.05f,0.05f) ;
            transform.DOScale(Vector3.zero, 0.35f).SetEase(Ease.OutQuart).OnComplete(()=>Destroy(gameObject));
            Disable();
            collision.gameObject.GetComponent<SwitchBehaviour>().enabled = true;
            Instantiate(switchVFX, collision.transform.position, Quaternion.identity);

        }
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.Active:
                Enable();
                break;
            case State.Disabled:
                Disable();
                break;
            default:
                break;
        }
    }

    private void OnEnable()
    {

        state=State.Active;
        if (isMainCharacter==false)
        {
            Enable();

        }
    }

    private void OnDisable()
    {
        state = State.Disabled;
    }



    void Disable()
    {
        if (outline) outline.SetActive(true);
        mainBody.SetActive(false);
    }

    void Enable()
    {
        if (outline) outline.SetActive(false);
        mainBody.SetActive(true);
        transform.gameObject.tag = "Untagged";
        var paralaxes = FindObjectsOfType<Parallax>();
        foreach (var para in paralaxes)
        {
            para.player = this.gameObject;
        }
        if (isMainCharacter==false)
        {
            FindObjectOfType<AudioManager>().Play("Switch");

        }
    }

}

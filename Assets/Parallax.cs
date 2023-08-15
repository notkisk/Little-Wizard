using DG.Tweening;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject player;
    public float parallaxEffect;

    public bool isMouse;
    public float parallaxMoveDuration=1f;
    // Start is called before the first frame update

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().gameObject;
        }
    }
    void Start()
    {
        startpos = isMouse ? 0f : player.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (player==null)
        {
            player = GameObject.FindGameObjectWithTag("Enemy");
        }
        if (isMouse)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            
            float temp = (mousePos.x * (1 - parallaxEffect));
            float dist = (mousePos.x * parallaxEffect);

            transform.DOMoveX(startpos + dist, parallaxMoveDuration).SetEase(Ease.OutQuart);
        }
        else
        {
            float temp = (player.transform.position.x * (1 - parallaxEffect));
            float dist = (player.transform.position.x * parallaxEffect);

            transform.DOMoveX(startpos + dist, parallaxMoveDuration).SetEase(Ease.OutQuart);
        }
        /*transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);*/

        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;
    }
}

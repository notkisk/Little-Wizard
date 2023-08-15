using DG.Tweening;
using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField]
    private GameObject hitVFX;
    Rigidbody2D rb;


    float _moveSpeed;
    public UnityEngine.Vector2 _moveDirection;

    public GameObject sparkles;

    public float sparklesSpawnRate=0.15f;
    float timeBtwSpawn;


    public bool isStopped = false;
    [SerializeField]
    private float CircleRadius;
    [SerializeField]
    private LayerMask myLayer;

    public float triggerThreshold;
    [HideInInspector]
    public bool isGreen, isBlue = false, isRed = false;

    public SpriteRenderer circleRenderer;
    public void Init(float moveSpeed,UnityEngine.Vector2 moveDirection)
    {
        _moveSpeed = moveSpeed;
        _moveDirection = moveDirection;
    }

    // Start is called before the first frame update
    void Awake()
    {
        isGreen = true;
        isBlue = false;
        isRed = false;
        rb = GetComponent<Rigidbody2D>();
        timeBtwSpawn = sparklesSpawnRate;
    }

    private void Update()
    {
        if (timeBtwSpawn<=0f)
        {
            Vector2 spawnPosition = (Vector2)transform.position+ Random.insideUnitCircle*1f;
            float randomZRotaion = Random.Range(0f,180f);
            Instantiate(sparkles, spawnPosition, Quaternion.Euler(new Vector3(0f, 0f, randomZRotaion)));
            timeBtwSpawn = sparklesSpawnRate;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = isStopped ? Vector2.zero: _moveDirection.normalized * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGreen)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);

                Destroy(GetComponent<CircleCollider2D>());
                Destroy(collision.GetComponent<Life>());
                this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                Destroy(collision);
                collision.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuart);
                //play hit vfx and audio
                FindObjectOfType<AudioManager>().Play("Hit");
                Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
                FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
            }
            else
            {
                if (!collision.gameObject.CompareTag("Spike")&&!collision.gameObject.CompareTag("Redirect")&&!collision.gameObject.CompareTag("RedEnemy")&&!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Finish"))
                {
                    CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);
                    Destroy(GetComponent<CircleCollider2D>());
                    //Destroy(gameObject);
                    this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
                    //play hit vfx and audio
                    FindObjectOfType<AudioManager>().Play("Hit");
                    Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
                    FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
                }
            }
        }
        else if (isBlue)
        {
            if (collision.gameObject.CompareTag("BlueEnemy"))
            {
                CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);

                Destroy(GetComponent<CircleCollider2D>());
                Destroy(collision.GetComponent<Life>());
                this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                Destroy(collision);
                collision.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuart);
                //play hit vfx and audio
                FindObjectOfType<AudioManager>().Play("Hit");
                Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
                FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
            }
            else
            {
                if (!collision.gameObject.CompareTag("Spike") && !collision.gameObject.CompareTag("Redirect") && !collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("RedEnemy")&& !collision.gameObject.CompareTag("Player")&&!collision.gameObject.CompareTag("Finish"))
                {
                    CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);
                    Destroy(GetComponent<CircleCollider2D>());
                    //Destroy(gameObject);
                    this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
                    //play hit vfx and audio
                    FindObjectOfType<AudioManager>().Play("Hit");
                    Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
                    FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
                }

            }
        }
        else if (isRed)
        {
            if (collision.gameObject.CompareTag("RedEnemy"))
            {
                CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);

                Destroy(GetComponent<CircleCollider2D>());
                Destroy(collision.GetComponent<Life>());
                this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                Destroy(collision);
                collision.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuart);
                //play hit vfx and audio
                FindObjectOfType<AudioManager>().Play("Hit");
                Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
                FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
            }
            else
            {
                if (!collision.gameObject.CompareTag("Spike") && !collision.gameObject.CompareTag("Redirect") && !collision.gameObject.CompareTag("Enemy")&& !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Finish"))
                {
                    CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);
                    Destroy(GetComponent<CircleCollider2D>());
                    //Destroy(gameObject);
                    this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
                    //play hit vfx and audio
                    FindObjectOfType<AudioManager>().Play("Hit");
                    Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
                    FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
                }
            }
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            CameraShaker.Instance.ShakeOnce(3f, 4f,0.05f,0.05f) ;
            Destroy(GetComponent<CircleCollider2D>());
            //Destroy(gameObject);
            this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
            //play hit vfx and audio
            FindObjectOfType<AudioManager>().Play("Hit");
            Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
            FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
        }





    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Redirect"))
        {
            if (Vector2.Distance(transform.position, collision.transform.position) <= triggerThreshold)
            {

                if (collision.GetComponent<Redirect>().NewDirection == Vector2.zero && isStopped == false)
                {
                    transform.position = collision.transform.position;
                    isStopped = true;
                    
                    //FindObjectOfType<AudioManager>().Play("Redirect");

                }
                else
                {
                    //FindObjectOfType<AudioManager>().Play("Redirect");
                    _moveDirection = collision.GetComponent<Redirect>().NewDirection;
                }

            }
        }
    }

    public bool IsOperlapping()
    {
        return Physics2D.OverlapCircle(transform.position, CircleRadius, myLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,
            CircleRadius);
    }

    public void KillOrb()
    {
        isGreen = true;
        isBlue = false;
        isRed = false;
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(gameObject);
        this.transform.DOScale(UnityEngine.Vector2.zero, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => Destroy(gameObject));
        //play hit vfx and audio
        FindObjectOfType<AudioManager>().Play("Hit");
        Instantiate(hitVFX, transform.position, UnityEngine.Quaternion.identity);
        FindObjectOfType<ShotHandler>().numberOfOrbs = 1;
    }
}

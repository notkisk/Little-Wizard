using DG.Tweening;
using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHandler : MonoBehaviour
{

    [SerializeField]
    private Snowball snowBall;
    [SerializeField]
    private Transform shotPosition;
    [SerializeField]
    private Transform playerGFX;
    [SerializeField]
    private float snowBallMoveSpeed;

    [SerializeField]
    private float shootRate;

    float timeBtwShot;

    Animator anim;
    public int numberOfOrbs = 1;
    public bool isTraveling = false;
    public Snowball snowBallTemp;
    public float travelSpeed;

    public GameObject circle;
    public GameObject sparkle;
    public float sparkleSpawnRate;

    float timeBtwSparkle;
    public float CircleRadius;
    public LayerMask myLayer;
    [SerializeField]
    private Vector3 circleOffset = Vector3.zero;

    public LayerMask enemyLayer;
    public GameObject deathFX;
    private void Awake()
    {
        timeBtwSparkle = sparkleSpawnRate;
        timeBtwShot = shootRate;
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
           
          if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.J))
                {
            if (numberOfOrbs==1)
                {
                FindObjectOfType<AudioManager>().Play("Shoot");

                if (GetComponentInChildren<PlayerController>().isGrounded)
                    {
                        anim.SetTrigger("Cast");
                    }
                    else
                    {
                        anim.SetTrigger("CastAerial");

                    }
                    timeBtwShot = shootRate;
                    Vector2 shotMoveDirection = playerGFX.localScale.x * Vector2.right;
                    Snowball _snowBall = Instantiate(snowBall, shotPosition.position, Quaternion.identity);
                    snowBallTemp = _snowBall;
                    _snowBall.Init(snowBallMoveSpeed, shotMoveDirection);
                    numberOfOrbs = 0;
                }
                else if (numberOfOrbs ==0)
                {
                    if (isTraveling)
                    {
                    if (IsOperlapping() == false)
                        {
                        FindObjectOfType<AudioManager>().Play("Cancell");

                        isTraveling = false;
                            GetComponentInChildren<Animator>().SetBool("isTraveling", false);
                            GetComponentInChildren<PlayerController>().enabled = true;
                            GetComponent<Rigidbody2D>().isKinematic = false;
                            GetComponent<CapsuleCollider2D>().isTrigger = false;
                            snowBallTemp.KillOrb();
                            transform.DOKill();
                        }
                    }
                    else
                    {
                        if (snowBallTemp.IsOperlapping() == false)
                        {
                        FindObjectOfType<AudioManager>().Play("Travel");

                        isTraveling = true;
                        GetComponentInChildren<Animator>().SetBool("isTraveling", true);
                        snowBallTemp.isStopped = true;
                        transform.DOMove(snowBallTemp.transform.position, travelSpeed).SetEase(Ease.OutQuart).SetSpeedBased(true).OnComplete(() => {

                                GetComponentInChildren<PlayerController>().enabled = true;
                                GetComponent<Rigidbody2D>().isKinematic = false;
                                GetComponent<CapsuleCollider2D>().isTrigger = false;
                                isTraveling = false;
                                snowBallTemp.KillOrb();
                                GetComponentInChildren<Animator>().SetBool("isTraveling", false);

                            });
                            GetComponentInChildren<PlayerController>().enabled = false;
                            GetComponent<Rigidbody2D>().isKinematic = true;
                            GetComponent<CapsuleCollider2D>().isTrigger = true;

                    }
              
                    }
                }

                 
          }
        if (snowBallTemp!=null)
        {
            if (Vector2.Distance(transform.position, snowBallTemp.transform.position) <= 0.35f)
            {
                isTraveling = false;
                GetComponentInChildren<Animator>().SetBool("isTraveling", false);
                GetComponentInChildren<PlayerController>().enabled = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                GetComponent<CapsuleCollider2D>().isTrigger = false;
                snowBallTemp.KillOrb();
                transform.DOKill();
            }
        }
        
        if (isTraveling)
        {
            circle.SetActive(true);

            if (timeBtwSparkle <= 0f)
            {
                timeBtwSparkle = sparkleSpawnRate;
                Vector2 spawnPosition = transform.position + Random.insideUnitSphere * 1.0f;
                float randromZRotation = Random.Range(0f, 180f);
                Quaternion randromRotation = Quaternion.Euler(0f, 0f, randromZRotation);
                Instantiate(sparkle, spawnPosition, randromRotation);
            }
            else
            {
                timeBtwSparkle -= Time.deltaTime;
            }

         
        }
        else
            circle.SetActive
                    (false);

    
    }

    public bool IsOperlapping()
    {
        Vector3 circlePosition = new Vector3(transform.position.x + circleOffset.x, transform.position.y + circleOffset.y,transform.position.z);
        
        return Physics2D.OverlapCircle(circlePosition, CircleRadius, myLayer);
    }

    public Collider2D IsCollidingWithEnemy()
    {
        Vector3 circlePosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        Collider2D enemy= Physics2D.OverlapCircle(circlePosition, CircleRadius, enemyLayer);
        return enemy;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isTraveling)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (snowBallTemp.isGreen)
                {
                    CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);

                    Destroy(collision.GetComponent<Life>());
                    Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                    Destroy(collision);
                    collision.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuart);
                    //play hit vfx and audio
                    FindObjectOfType<AudioManager>().Play("Hit");
                }
              

            }
             else if(collision.gameObject.CompareTag("BlueEnemy"))
            {
                if (snowBallTemp.isBlue)
                {
                    CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);

                    Destroy(collision.GetComponent<Life>());
                    Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                    Destroy(collision);
                    collision.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuart);
                    //play hit vfx and audio
                    FindObjectOfType<AudioManager>().Play("Hit");
                }
      
            }
            else if (collision.gameObject.CompareTag("RedEnemy"))
            {
                if (snowBallTemp.isRed)
                {
                    CameraShaker.Instance.ShakeOnce(3f, 4f, 0.05f, 0.05f);

                    Destroy(collision.GetComponent<Life>());
                    Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
                    Destroy(collision);
                    collision.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuart);
                    //play hit vfx and audio
                    FindObjectOfType<AudioManager>().Play("Hit");
                }
            }
        }
       
       
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 circleDrawPosition = new Vector3(transform.position.x + circleOffset.x, transform.position.y + circleOffset.y, transform.position.z);
        Gizmos.DrawWireSphere(circleDrawPosition, CircleRadius);
    }
}

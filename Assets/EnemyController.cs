using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;
    public float minimumMoveDistance;
    [HideInInspector]
    public bool canMove=true;
    Animator anim;
    Rigidbody2D rb;

    PlayerController _player;
    public float stabThreshold;
    public float difThreshold;
    public GameObject deathFX;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player != null)
        {
            var distanceToPlayer = Vector2.Distance(this.transform.position, _player.transform.position);
            if (canMove)
            {
                if (distanceToPlayer <= minimumMoveDistance&&distanceToPlayer>stabThreshold)
                {
                   
                 
                    if (!ApproximatlyEquale(transform.position.x,_player.transform.position.x,difThreshold))
                    {
                        if (distanceToPlayer > stabThreshold)
                        {
                            anim.SetBool("IsWalking", true);

                        }
                        if (this.transform.position.x + difThreshold > _player.transform.position.x)
                        {
                            rb.velocity = new Vector2(-moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        else if (this.transform.position.x < _player.transform.position.x)
                        {
                            rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(-1f, 1f, 1f);

                        }
                    }
                    else
                    {
                        rb.velocity =new Vector2(0f,rb.velocity.y);
                        anim.SetBool("IsWalking", false);

                    }


                }
                else
                {
                    rb.velocity = new Vector2(0f,rb.velocity.y);
                    anim.SetBool("IsWalking", false);
                }
            }
        }

    }

    public void Freeze()
    {
        anim.SetBool("IsFreezing", true);
        canMove=false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        Destroy(GetComponent<Life>());
        FindObjectOfType<AudioManager>().Play("Death");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canMove)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Freeze();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minimumMoveDistance);
    }

    bool ApproximatlyEquale(float a,float b,float threshold)
    {
        return Mathf.Abs(a - b) < threshold;
    }
}

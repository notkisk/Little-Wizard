using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{

    enum state { enabled,disabled };
    state currentState;
    [Header("Movement")]
    [HideInInspector]public Rigidbody2D _rb;
    public float maxMoveSpeed;
    public float _acceleration, _deacceleration;
    public Ease easeType = Ease.Linear;
   [HideInInspector] public float _horizontalInput;
    float currentMoveSpeed;
    [HideInInspector]public bool canMove = true;
    bool isFacingRight=true;
    [Header("Jumping")]
    public LayerMask whatIsGround;
    public Transform footPosition;
    public float jumpForce;
    public float groundCheckLength;
    public float groundCheckGap = 0.025f;
    public float hangTime = 0.2f;
    public float groundCheckRadius;
    [HideInInspector]
    public bool isGrounded;
    private bool groundedLastFrame;
    private float hangTimeCounter;
    private Animator _anim;


    [Header("Effects")]
    public GameObject jumpingEffect;
    public GameObject landingEffect;
    public Vector2 landingEffectOffset;
    public float footStepRate;
    public GameObject deathEffect;

    float timeBtwFootStep;
    public Vector3 jumpingFxOffset;

    int maxJump=1;
    int jumpCount;
    //public Color _disabledColor;
    //private CapsuleCollider2D _myCapsuleCollider;
    //private BoxCollider2D _myBoxCollider;
    //Rigidbody2D _parentBody;

    //Color _startingColor;
    float velocityLastFrame;
    float currentVerticalSpeed;


    float _verticalInput;
    public float maxYVelocity;


    [Header("Chracteristics")]
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool canFly;

    Vector3 startingScale;
    public GameObject footStepVFX;

    private void Awake()
    {
        timeBtwFootStep = footStepRate;
        _rb = GetComponentInParent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        startingScale = transform.parent.localScale;



    }

    // Update is called once per frame
    private void LateUpdate()
    {
        groundedLastFrame = isGrounded;
        velocityLastFrame = _rb.velocity.y;
    }
    void Update()
    {


        _horizontalInput =Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        isGrounded = Grounded();
        if (canJump==true)
        {
            if (isGrounded)
            {
                hangTimeCounter = hangTime;
            }
            else
            {
                hangTimeCounter -= Time.deltaTime;
            }
        }
      
        FlipHandler();
     
        if (canJump&&!canFly)
        {
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (hangTimeCounter > 0f)
                    {
                        FindObjectOfType<AudioManager>().Play("Jump");
                        Instantiate(jumpingEffect, footPosition.position+jumpingFxOffset, Quaternion.identity);
                        _rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
                        //_anim.SetBool("isJumping", true);
                        transform.parent.DOScale(new Vector3(transform.parent.localScale.x - 0.25f, transform.parent.localScale.y + 0.25f, 1f), 0.1f).SetEase(Ease.Linear).OnComplete(() => transform.parent.DOScale(startingScale, 0.1f).SetEase(Ease.Linear));
                    }
                }
        }
   

        if (Mathf.Approximately(_horizontalInput,0f))
        {
            _anim.SetBool("isWalking", false);

        }

        if (canFly == false)
        {
            if (isGrounded && !groundedLastFrame)
            {

                Instantiate(landingEffect, (Vector2)footPosition.position + landingEffectOffset, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("Land");
                transform.parent.DOScale(new Vector3(startingScale.x+0.1f,startingScale.y-0.1f, 1f), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                transform.parent.DOScale(startingScale, 0.1f).SetEase(Ease.Linear));
            }
        }
         
        if (canMove && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Pushing"))
        {
            if (isGrounded && !Mathf.Approximately(_horizontalInput, 0f) && timeBtwFootStep <= 0f&&canFly==false)
            {
                timeBtwFootStep = footStepRate;
                FindObjectOfType<AudioManager>().Play("Footstep");
                Instantiate(footStepVFX, footPosition.position, Quaternion.identity);
            }
            else
            {
                timeBtwFootStep -= Time.deltaTime;
            }
        }

     
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            DOVirtual.Float(currentMoveSpeed, _horizontalInput * maxMoveSpeed, _horizontalInput != 0f ? _acceleration : _deacceleration, value => { currentMoveSpeed = value; }).SetEase(easeType);

            if (canFly==false)
            {
                _rb.velocity = new Vector2(currentMoveSpeed, _rb.velocity.y);

            }
            else
            {
                Vector2 moveVector=new Vector2(_horizontalInput,_verticalInput).normalized* maxMoveSpeed;
                _rb.velocity = moveVector;

            }
            if (isGrounded)
            {
                _anim.SetBool("isWalking", true);

            }
            else
            {
                _anim.SetBool("isWalking", false);

            }
        }

    }

   private void FlipHandler()
    {
        if (!isFacingRight&&_horizontalInput>0f)
        {
            Flip();
        }
        else if (isFacingRight&&Mathf.Sign(_horizontalInput)==-1f)
        {
            Flip();
        }

    }

   

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

    }


    public bool Grounded()
    {
        Vector2 lineStart = new Vector2(footPosition.position.x, footPosition.position.y - groundCheckGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y - groundCheckLength);
        return Physics2D.OverlapCircle(lineEnd, groundCheckRadius, whatIsGround);
    }

 
    private void OnDrawGizmos()
    {
        
        Vector2 lineStart = new Vector2(footPosition.position.x, footPosition.position.y - groundCheckGap);
        Vector2 lineEnd = new Vector2(lineStart.x, lineStart.y - groundCheckLength);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lineEnd, groundCheckRadius);

    }

}
